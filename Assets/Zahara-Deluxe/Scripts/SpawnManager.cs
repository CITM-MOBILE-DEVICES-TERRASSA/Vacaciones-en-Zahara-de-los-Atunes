using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ElephantPrefab;
    public GameObject PrincesaPrefab;

    public float spawnRangeX = 850;
    public float spawnPosY = 40;
    public float spawnPosY2 = -230;
    public float spawnPosY3 = -500;

    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    private List<float> spawnHeights;

    void Start()
    {
        spawnHeights = new List<float> { spawnPosY, spawnPosY2, spawnPosY3 };
        InvokeRepeating("SpawnObject", startDelay, spawnInterval);
    }

    void SpawnObject()
    {
        GameObject prefabToSpawn = ChoosePrefab();
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instanciar el objeto con una rotación inicial inclinada
        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.Euler(0, 0, 90));

        // Aplicar la animación de rotación hacia la posición recta
        StartCoroutine(RotateToUpright(spawnedObject.transform));
    }

    GameObject ChoosePrefab()
    {
        float chance = Random.value;
        return chance <= 0.7f ? ElephantPrefab : PrincesaPrefab;
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomY = spawnHeights[Random.Range(0, spawnHeights.Count)];
        return new Vector3(randomX, randomY, 917);
    }

    IEnumerator RotateToUpright(Transform objTransform)
    {
        Quaternion startRotation = objTransform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);
        float duration = 1f; // Tiempo para enderezarse
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            objTransform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / duration);
            yield return null;
        }

        objTransform.rotation = endRotation; // Asegurar rotación exacta
    }
}
