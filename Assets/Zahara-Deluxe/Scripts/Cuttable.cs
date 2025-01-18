using UnityEngine;

public class Cuttable : MonoBehaviour
{
    public GameObject topHalfPrefab; // Mitad superior del objeto
    public GameObject bottomHalfPrefab; // Mitad inferior del objeto

    public void Cut()
    {
        // Instancia las dos mitades
        GameObject topHalf = Instantiate(topHalfPrefab, new Vector3(transform.position.x+0.1f,transform.position.y+0.5f,transform.position.z), transform.rotation);
        GameObject bottomHalf = Instantiate(bottomHalfPrefab, new Vector3(transform.position.x-0.1f,transform.position.y,transform.position.z), transform.rotation);

        // Agrega físicas a las mitades
        Rigidbody topRb = topHalf.GetComponent<Rigidbody>();
        Rigidbody bottomRb = bottomHalf.GetComponent<Rigidbody>();

        if (topRb != null) topRb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
        if (bottomRb != null) bottomRb.AddForce(Vector3.left * 5f, ForceMode.Impulse);

        // Destruye el objeto original
        Destroy(gameObject);
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            Cut();
        }
    }
}
