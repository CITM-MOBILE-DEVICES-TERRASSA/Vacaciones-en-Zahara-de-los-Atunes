using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Knife : MonoBehaviour
{
    public float cutForce = 10f; // Fuerza de corte (si aplica física)
    public float cutAngle = 90f; // Ángulo de rotación para el corte
    public float cutSpeed = 5f; // Velocidad del corte
    public float inkDuration = 3f; // Duración del efecto de tinta
    public float fadeSpeed = 1f; // Velocidad a la que se desvanece la tinta
    public AudioSource KnifeCut; 
    public AudioSource FishSquash;

    private bool isCutting = false; // Para evitar cortes múltiples al mismo tiempo
    private bool hasCut = false;
    private bool isInked = false;
    private Quaternion initialRotation; // Rotación inicial del cuchillo
    ScoreL1 score;

    private Image inkOverlay; // Imagen negra semi-transparente para simular la tinta
    private Color originalColor;
    Timer timer;

    void Start()
    {
        // Guarda la rotación inicial del cuchillo
        initialRotation = transform.rotation;
        score = GameObject.Find("ScoreManager").GetComponent<ScoreL1>();
        timer = GameObject.Find("Canvas").GetComponent<Timer>();

        // Busca el objeto InkOverlay en el Canvas
        inkOverlay = GameObject.Find("Ink")?.GetComponent<Image>();
        if (inkOverlay == null)
        {
            Debug.LogError("No se encontró InkOverlay en la escena. Asegúrate de tener un objeto llamado 'InkOverlay'.");
            return;
        }

        // Configura el color inicial como transparente
        originalColor = inkOverlay.color;
        originalColor.a = 0;
        inkOverlay.color = originalColor;
    }

    void Update()
    {
        // Detecta el clic izquierdo para realizar el corte
        if (Input.GetMouseButtonDown(0) && !isCutting)
        {
            StartCoroutine(PerformCut());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Si el cuchillo colisiona con un calamar (con etiqueta "Squid"), aplica el efecto de tinta
        if (other.gameObject.CompareTag("Squid") && !isInked)
        {
            FishSquash.Play();  
            StartCoroutine(ApplyInkEffect());
            timer.timeLeft -= 10;
            Destroy(other.gameObject);
        }

        // Realiza el corte si colisiona con un objeto cortable
        if (other.gameObject.CompareTag("Cuttable") && !hasCut && isCutting && !isInked)
        {
            
            CutObject(other.gameObject);
            FishSquash.Play();
            hasCut = true;
        }
        else if (other.gameObject.CompareTag("Cinta") && !hasCut && isCutting)
        {
            hasCut = true;
        }
    }

    void CutObject(GameObject obj)
    {
        // Lógica para cortar el objeto
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
        if(!isInked){
            KnifeCut.Play();
            isCutting = true;

            // Rotación hacia abajo (corte)
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x - cutAngle, transform.eulerAngles.y, transform.eulerAngles.z);
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, cutSpeed * Time.deltaTime);
                yield return null;
            }
            // Asegúrate de llegar exactamente al ángulo deseado
            transform.rotation = targetRotation;

            // Espera un breve momento en la posición de corte
            yield return new WaitForSeconds(0.1f);

            // Rotación de vuelta a la posición inicial
            while (Quaternion.Angle(transform.rotation, initialRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, cutSpeed * Time.deltaTime);
                yield return null;
            }

            // Asegúrate de llegar exactamente a la posición inicial
            transform.rotation = initialRotation;

            isCutting = false;
            hasCut = false;
        }
        
    }

    IEnumerator ApplyInkEffect()
    {
        isInked = true;

        // Oscurece gradualmente la pantalla
        float elapsedTime = 0f;
        while (elapsedTime < inkDuration / 5)
        {
            originalColor.a = Mathf.Clamp01(elapsedTime / (inkDuration / 5));
            inkOverlay.color = originalColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Mantén la opacidad máxima durante un breve periodo
        yield return new WaitForSeconds(inkDuration / 2);

        // Desvanece gradualmente la pantalla
        elapsedTime = 0f;
        while (elapsedTime < inkDuration / 3)
        {
            originalColor.a = Mathf.Clamp01(1 - (elapsedTime / (inkDuration / 2)));
            inkOverlay.color = originalColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Restaura la transparencia completa
        originalColor.a = 0;
        inkOverlay.color = originalColor;
        isInked = false;
    }
}
