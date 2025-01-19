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

    public int maxObjects = 15; 
    public float minDistance = 300f;
    public float objectLifetime = 10f;

    private float startDelay = 2;
    private float spawnInterval = 1f;
    private List<float> spawnHeights;
    private List<GameObject> spawnedObjects;

    void Start()
    {
        spawnHeights = new List<float> { spawnPosY, spawnPosY2, spawnPosY3 };
        spawnedObjects = new List<GameObject>();
        InvokeRepeating("SpawnObject", startDelay, spawnInterval);
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

        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.Euler(90, -90, 0));
        spawnedObjects.Add(spawnedObject);

        StartCoroutine(RotateToUpright(spawnedObject.transform));
        StartCoroutine(DestroyAfterDelay(spawnedObject));
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
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        float randomY = spawnHeights[Random.Range(0, spawnHeights.Count)];
        return new Vector3(randomX, randomY, 0);
    }

    IEnumerator RotateToUpright(Transform objTransform)
    {
        Quaternion startRotation = objTransform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);
        float duration = .2f; 
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            objTransform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / duration);
            yield return null;
        }

        objTransform.rotation = endRotation; 
    }
}
