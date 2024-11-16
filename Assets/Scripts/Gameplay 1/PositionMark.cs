using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMark : MonoBehaviour
{
    void Start()
    {
        float normalizedX = GetRandomXNormalized();
        Debug.Log("Punto aleatorio en el eje X (normalizado): " + normalizedX);
    }

    float GetRandomXNormalized()
    {
        Renderer renderer = GetComponent<Renderer>();

        Bounds bounds = renderer.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);

        float normalizedX = Mathf.InverseLerp(bounds.min.x, bounds.max.x, randomX);

        return normalizedX;
    }
}
