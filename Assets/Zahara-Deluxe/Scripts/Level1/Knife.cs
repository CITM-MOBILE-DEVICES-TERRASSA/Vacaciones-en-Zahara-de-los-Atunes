using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Knife : MonoBehaviour
{
    public float cutForce = 10f;
    public float cutAngle = 90f;
    public float cutSpeed = 5f;
    public float inkDuration = 3f;
    public float fadeSpeed = 1f;
    public AudioSource KnifeCut;
    public AudioSource FishSquash;

    private bool isCutting = false;
    private bool hasCut = false;
    private bool isInked = false;
    private bool isSpeedBoosted = false;
    private Quaternion initialRotation;
    ScoreL1 score;

    private GameObject ink;
    private Image inkOverlay;
    private Color originalColor;
    Timer timer;

    private float originalTimeScale;

    void Start()
    {
        initialRotation = transform.rotation;
        score = GameObject.Find("ScoreManager").GetComponent<ScoreL1>();
        timer = GameObject.Find("Overlay").GetComponent<Timer>();

        ink = GameObject.Find("Ink");
        inkOverlay = ink.GetComponent<Image>();

        originalColor = inkOverlay.color;
        originalColor.a = 0;
        inkOverlay.color = originalColor;

        originalTimeScale = Time.timeScale;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isCutting)
        {
            StartCoroutine(PerformCut());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Squid") && !isInked)
        {
            FishSquash.Play();
            StartCoroutine(ApplyInkEffect());
            timer.timeLeft -= 10;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Poison"))
        {
            FishSquash.Play();
            score.updateScore(-2);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Speed") && !isSpeedBoosted)
        {
            FishSquash.Play();
            StartCoroutine(ApplySpeedBoost());
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Cuttable") && !hasCut && isCutting && !isInked)
        {
            CutObject(other.gameObject);
            FishSquash.Play();
            
        }
        else if (other.gameObject.CompareTag("Cinta") && !hasCut && isCutting)
        {
            hasCut = true;
        }
    }

    IEnumerator ApplySpeedBoost()
    {
        isSpeedBoosted = true;

        Time.timeScale = originalTimeScale * 2f;

        yield return new WaitForSeconds(5f);

        Time.timeScale = originalTimeScale;

        isSpeedBoosted = false;
    }

    void CutObject(GameObject obj)
    {
        Debug.Log("Objeto cortado: " + obj.name);
        Cuttable cuttable = obj.GetComponent<Cuttable>();
        if (cuttable != null)
        {
            cuttable.Cut();
            score.updateScore(1);
        }
    }

    private System.Collections.IEnumerator PerformCut()
    {
        if (!isInked)
        {
            KnifeCut.Play();
            isCutting = true;

            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x - cutAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, cutSpeed * Time.deltaTime);
                yield return null;
            }
            transform.rotation = targetRotation;

            yield return new WaitForSeconds(0.1f);

            while (Quaternion.Angle(transform.rotation, initialRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, cutSpeed * Time.deltaTime);
                yield return null;
            }

            transform.rotation = initialRotation;

            isCutting = false;
            hasCut = false;
        }
    }

    IEnumerator ApplyInkEffect()
    {
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;
        ink.transform.position = new Vector3(cameraPosition.x, cameraPosition.y - 1, cameraPosition.z + 3);
        isInked = true;

        float elapsedTime = 0f;
        while (elapsedTime < inkDuration / 5)
        {
            originalColor.a = Mathf.Clamp01(elapsedTime / (inkDuration / 5));
            inkOverlay.color = originalColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(inkDuration / 2);

        elapsedTime = 0f;
        while (elapsedTime < inkDuration / 3)
        {
            originalColor.a = Mathf.Clamp01(1 - (elapsedTime / (inkDuration / 2)));
            inkOverlay.color = originalColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        originalColor.a = 0;
        inkOverlay.color = originalColor;
        isInked = false;
    }
}