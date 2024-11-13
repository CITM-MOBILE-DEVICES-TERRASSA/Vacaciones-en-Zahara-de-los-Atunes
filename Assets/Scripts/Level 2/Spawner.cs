using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Prefabs para cada tipo de objeto
    public GameObject elefantePrefab;
    public GameObject monarcaPrefab;
    public GameObject pedroSanchezPrefab;
    public GameObject reinaSofiaPrefab;

    // Referencia al Canvas donde se generarán los objetos
    public Canvas spawnCanvas;

    // Duración total del spawn en segundos (2 minutos y medio = 150 segundos)
    private float totalSpawnDuration = 150f;
    private float elapsedTime = 0f;

    // Intervalo de spawn (medio segundo para elefantes, 1 segundo para monarcas)
    private float elefanteSpawnInterval = 0.5f;
    private float monarcaSpawnInterval = 1.0f;

    // Conteo de spawn específicos
    private int pedroSanchezSpawnCount = 0;
    private int maxPedroSanchezCount = 3;
    private bool hasSpawnedReinaSofia = false;

    void Start()
    {
        // Iniciar los ciclos de spawn
        StartCoroutine(SpawnElefantes());
        StartCoroutine(SpawnMonarcas());
        StartCoroutine(SpawnSpecialCharacters());
    }

    IEnumerator SpawnElefantes()
    {
        while (elapsedTime < totalSpawnDuration)
        {
            Spawn(elefantePrefab);
            yield return new WaitForSeconds(elefanteSpawnInterval);
            elapsedTime += elefanteSpawnInterval;
        }
    }

    IEnumerator SpawnMonarcas()
    {
        while (elapsedTime < totalSpawnDuration)
        {
            Spawn(monarcaPrefab);
            yield return new WaitForSeconds(monarcaSpawnInterval);
        }
    }

    IEnumerator SpawnSpecialCharacters()
    {
        while (elapsedTime < totalSpawnDuration)
        {
            // Spawn de Pedro Sánchez (máximo 3)
            if (pedroSanchezSpawnCount < maxPedroSanchezCount)
            {
                Spawn(pedroSanchezPrefab);
                pedroSanchezSpawnCount++;
            }

            // Spawn de Reina Sofía (solo 1 vez)
            if (!hasSpawnedReinaSofia)
            {
                Spawn(reinaSofiaPrefab);
                hasSpawnedReinaSofia = true;
            }

            yield return new WaitForSeconds(totalSpawnDuration); // Espera hasta el final del tiempo de spawn
        }
    }

    void Spawn(GameObject prefab)
    {
        // Generar una posición aleatoria dentro del Canvas
        RectTransform canvasRect = spawnCanvas.GetComponent<RectTransform>();

        // Asegurarse de que la posición esté dentro de los límites del Canvas
        float x = Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2);
        float y = Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2);

        // Instancia el objeto en la posición calculada
        GameObject spawnedObject = Instantiate(prefab, spawnCanvas.transform);

        // Ajusta la posición del objeto en el espacio del Canvas
        RectTransform rectTransform = spawnedObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);

        // Asegurarse de que el objeto esté por encima de otros elementos del Canvas
        rectTransform.SetAsLastSibling();  // Esto coloca el objeto en el "top layer" del Canvas
    }
}
