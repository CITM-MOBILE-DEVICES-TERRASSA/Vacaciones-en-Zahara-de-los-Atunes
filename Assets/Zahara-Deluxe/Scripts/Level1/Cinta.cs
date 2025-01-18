using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 2f; // Velocidad inicial
    public float speedIncreaseRate = 0.05f; // Cantidad por la cual aumenta la velocidad cada segundo

    private void Update()
    {
        // Incrementa la velocidad con el tiempo
        speed += speedIncreaseRate * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 force = transform.forward * -speed;
            rb.velocity = force;
        }
    }
}
