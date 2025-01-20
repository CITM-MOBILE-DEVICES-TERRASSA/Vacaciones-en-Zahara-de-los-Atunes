using UnityEngine;

public class Cuttable : MonoBehaviour
{
    public GameObject topHalfPrefab; // Mitad superior del objeto
    public GameObject bottomHalfPrefab; // Mitad inferior del objeto
    private GameObject topHalf;
    private GameObject bottomHalf;
    public void Cut()
    {
        // Instancia las dos mitades
        topHalf = Instantiate(topHalfPrefab, new Vector3(transform.position.x,transform.position.y+0.1f,transform.position.z), transform.rotation);
        bottomHalf = Instantiate(bottomHalfPrefab, new Vector3(transform.position.x-0.2f,transform.position.y+0.1f,transform.position.z), transform.rotation);

        // Agrega físicas a las mitades
        Rigidbody topRb = topHalf.GetComponent<Rigidbody>();
        Rigidbody bottomRb = bottomHalf.GetComponent<Rigidbody>();

        //if (topRb != null) topRb.AddForce(Vector3.up * 1f, ForceMode.Impulse);
        //if (bottomRb != null) bottomRb.AddForce(Vector3.up * 1f, ForceMode.Impulse);

        if (topRb != null) topRb.AddForce(Vector3.right * 5f, ForceMode.Impulse);
        if (bottomRb != null) bottomRb.AddForce(Vector3.left * 5f, ForceMode.Impulse);

        // Destruye el objeto original
        Destroy(gameObject);
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.C)){
            Cut();
        }
        if(topHalf != null && bottomHalf != null){
            if(topHalf.transform.position.y < -10){
                Destroy(topHalf);
            }
            if(bottomHalf.transform.position.y < -10){
                Destroy(bottomHalf);
            }
        }
    }
}
