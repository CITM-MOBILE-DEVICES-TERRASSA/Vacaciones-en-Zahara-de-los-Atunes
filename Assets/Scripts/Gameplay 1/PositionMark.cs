using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMark : MonoBehaviour
{
    public GameObject prefab; 
    private void Start()
    {
        CreatePoint();
    }
    

    
        public void CreatePoint()
        {
            if (prefab == null)
            {
                Debug.LogError("No se ha asignado un prefab en el inspector.");
                return;
            }

            Renderer renderer = GetComponent<Renderer>();
            if (renderer == null)
            {
                Debug.LogError("El objeto no tiene un componente Renderer para calcular los límites.");
                return;
            }

            float minX = renderer.bounds.min.x;
            float maxX = renderer.bounds.max.x;

            float randomX = Random.Range(minX, maxX);
            float randomy = 0.5f;

            Vector3 spawnPosition = new Vector3(randomX, transform.position.y + randomy, transform.position.z);
            
            Instantiate(prefab, spawnPosition, Quaternion.identity);
        }

    

}
