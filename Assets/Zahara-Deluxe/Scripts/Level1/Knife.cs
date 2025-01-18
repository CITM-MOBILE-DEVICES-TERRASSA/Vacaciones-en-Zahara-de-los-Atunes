using UnityEngine;

public class Knife : MonoBehaviour
{
    public float cutForce = 10f; // Fuerza de corte (si aplica física)
    public float cutAngle = 90f; // Ángulo de rotación para el corte
    public float cutSpeed = 5f; // Velocidad del corte
    private bool isCutting = false; // Para evitar cortes múltiples al mismo tiempo
    private bool hasCut = false;
    private Quaternion initialRotation; // Rotación inicial del cuchillo
    ScoreL1 score;
    void Start()
    {
        // Guarda la rotación inicial del cuchillo
        initialRotation = transform.rotation;
        score = GameObject.Find("ScoreManager").GetComponent<ScoreL1>();
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
        if (other.gameObject.CompareTag("Cuttable") && !hasCut)
        {
            CutObject(other.gameObject);
            hasCut=true;
        }
        else if (other.gameObject.CompareTag("Cinta") && !hasCut)
        {
            Debug.Log("hit");
            hasCut=true;
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
        hasCut=false;
    }
}
