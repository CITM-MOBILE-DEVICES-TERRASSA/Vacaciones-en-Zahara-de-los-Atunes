using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedFish : MonoBehaviour
{
    public float additionalSpeed = 2f; // Multiplicador de velocidad
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("No se encontr� un Rigidbody en el objeto. Por favor, a�ade un componente Rigidbody.");
            return;
        }
    }

    void FixedUpdate()
    {
        // Obtener la velocidad actual
        Vector3 currentVelocity = rb.velocity;

        // Si el pez se est� moviendo hacia la derecha (est� en la cinta)
        if (currentVelocity.x > 0)
        {
            // Multiplicar la velocidad actual por el factor adicional
            rb.velocity = new Vector3(currentVelocity.x * additionalSpeed, currentVelocity.y, currentVelocity.z);
        }
    }
}