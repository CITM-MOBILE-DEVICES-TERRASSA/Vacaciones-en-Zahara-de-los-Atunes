using System;
using System.Collections;
using System.Collections.Generic;
using Parabox.CSG;
using UnityEngine;

public class CuttingScript : MonoBehaviour
{
    public GameObject fish;
    public Transform marker; 
    public int cutScore;
    private ScoreManagerLevel scoremanagerlevel;
    public float distanceX;
    
    // Start is called before the first frame update
    void Start()
    {
           cutScore = 0;
           scoremanagerlevel = FindObjectOfType<ScoreManagerLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformCut()
{
    if (fish != null)
    {
        // Realizamos la operación de corte
        var m = CSG.Subtract(fish, gameObject);

        fish.GetComponent<MeshFilter>().sharedMesh = m.mesh;
        fish.GetComponent<MeshRenderer>().sharedMaterials = m.materials.ToArray();

        Destroy(fish.GetComponent<Collider>());
        fish.AddComponent<MeshCollider>().convex = true;

        if (marker != null)
        {
           // Calculamos la distancia absoluta en el eje X
           distanceX = Mathf.Abs(transform.position.x - marker.position.x);
    
            // Calculamos la puntuación proporcional a la distancia absoluta
           cutScore = Mathf.RoundToInt(distanceX * 200);
           scoremanagerlevel.UpdateScoreLevel1(cutScore);
       }
       else
       {
       }

        fish.transform.position = Vector3.zero;
    }
}


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cuttable"))
        {
            fish = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == fish)
        {
            fish = null;
        }
    }
}
