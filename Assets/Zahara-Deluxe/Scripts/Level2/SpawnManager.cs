using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ElephantPrefab;
    public GameObject PrincesaPrefab;

    public float spawnRangeX = 850;
    public float spawnPosY = 20;
    public float spawnPosY2 = -190;
    public float spawnPosY3 = -420;

    [SerializeField] private BushConfig bushConfig;
    private float[] rowWidths = new float[3];

    public int maxObjects = 15; 
    public float minDistance = 300f;
    public float objectLifetime = 10f;

    private float startDelay = 2;
    private float spawnInterval = 1f;
    private List<float> spawnHeights;
    private List<GameObject> spawnedObjects;

    private const int STARTING_SORT_ORDER = 1000;
    private const int SORT_ORDER_LAYER_DIFFERENCE = 100;
    public static SpawnManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawnHeights = new List<float> { spawnPosY, spawnPosY2, spawnPosY3 };
        spawnedObjects = new List<GameObject>();
        InitializeRowWidths();

        // Iniciar spawns
        InvokeRepeating("SpawnObject", startDelay, spawnInterval);

        // Iniciar el ajuste dinámico de spawn y lifetime
        StartCoroutine(AdjustSpawnAndLifetime());
    }

    private void InitializeRowWidths()
    {
        if (bushConfig == null)
        {
            Debug.LogError("BushConfig no est� asignado en SpawnManager!");
            return;
        }

        float currentScale = bushConfig.baseScale;
        for (int i = 2; i >= 0; i--)  // Comenzamos desde la fila superior
        {
            float totalWidth = bushConfig.bushesPerRow * bushConfig.bushSpacing * currentScale;
            rowWidths[i] = totalWidth / 2;  // Dividimos entre 2 porque spawnRangeX es +/-
            currentScale *= bushConfig.depthScaleFactor;
        }
    }

    void SpawnObject()
    {
        if (spawnedObjects.Count >= maxObjects)
        {
            spawnedObjects.RemoveAll(obj => obj == null);
            if (spawnedObjects.Count >= maxObjects) return; 
        }

        GameObject prefabToSpawn = ChoosePrefab();
        Vector3 spawnPosition = GetValidSpawnPosition();

        if (spawnPosition == Vector3.zero)
        {
            return;
        }

        // Define la posición inicial más abajo de la pantalla
        Vector3 startPosition = spawnPosition - new Vector3(0, 30, 0); // Ajusta el valor según la distancia

        // Instancia el objeto en la posición inicial
        GameObject spawnedObject = Instantiate(prefabToSpawn, startPosition, Quaternion.identity);

        SpriteRenderer spriteRenderer = spawnedObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            int rowIndex;
            if (Mathf.Approximately(spawnPosition.y, spawnPosY3))
                rowIndex = 0;
            else if (Mathf.Approximately(spawnPosition.y, spawnPosY2))
                rowIndex = 1;
            else
                rowIndex = 2;

            spriteRenderer.sortingOrder = STARTING_SORT_ORDER - (rowIndex * SORT_ORDER_LAYER_DIFFERENCE);
        }

        // Mueve el objeto hacia su posición final
        spawnedObjects.Add(spawnedObject);
        StartCoroutine(MoveToUpright(spawnedObject.transform, startPosition, spawnPosition));

        // Destruye después de un tiempo
        StartCoroutine(DestroyAfterDelay(spawnedObject));
    }

    IEnumerator AdjustSpawnAndLifetime()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Cada 5 segundos ajustar los valores

            // Reducir el tiempo de spawn y ajustar el tiempo de vida al mismo valor
            if (spawnInterval > 0.3f)
            {
                spawnInterval = Mathf.Max(0.3f, spawnInterval - 0.1f);
                objectLifetime = spawnInterval; // Igualar el tiempo de vida al intervalo de spawn
            }
            else
            {
                Debug.Log("SpawnInterval y ObjectLifetime alcanzaron el límite mínimo.");
            }

            // Actualizar el intervalo de Invocar para reflejar el nuevo spawnInterval
            CancelInvoke("SpawnObject");
            InvokeRepeating("SpawnObject", spawnInterval, spawnInterval);

            Debug.Log($"SpawnInterval y ObjectLifetime sincronizados: {spawnInterval}");
        }
    }


    IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(objectLifetime);

        if (obj != null)
        {
            spawnedObjects.Remove(obj);
            Destroy(obj);
        }
    }

    GameObject ChoosePrefab()
    {
        float chance = Random.value;
        return chance <= 0.7f ? ElephantPrefab : PrincesaPrefab;
    }

    Vector3 GetValidSpawnPosition()
    {
        int maxAttempts = 30; 

        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 candidatePosition = GetRandomSpawnPosition();
            bool positionIsValid = true;

            foreach (GameObject obj in spawnedObjects)
            {
                if (obj != null && Vector3.Distance(obj.transform.position, candidatePosition) < minDistance)
                {
                    positionIsValid = false;
                    break;
                }
            }

            if (positionIsValid) return candidatePosition;
        }

        return Vector3.zero;
    }

    Vector3 GetRandomSpawnPosition()
    {
        int rowIndex = Random.Range(0, spawnHeights.Count);
        float rowSpawnRange = rowWidths[rowIndex];
        
        // Usamos el ancho espec�fico de la fila para el spawn
        float randomX = Random.Range(-rowSpawnRange, rowSpawnRange);
        float randomY = spawnHeights[rowIndex];
        
        return new Vector3(randomX, randomY, 0);
    }

    IEnumerator MoveToUpright(Transform objTransform, Vector3 startPosition, Vector3 endPosition)
    {
        float duration = .5f; // Duración del movimiento (en segundos)
        float elapsed = 0;

        objTransform.position = startPosition;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            // Interpolación suave entre la posición inicial y final
            objTransform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);

            yield return null;
        }

        // Asegurarse de que el objeto termine exactamente en la posición final
        objTransform.position = endPosition;
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnObject");
    }
}
