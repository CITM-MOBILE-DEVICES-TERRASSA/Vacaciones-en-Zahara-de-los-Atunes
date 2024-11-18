using System;
using System.Collections;
using System.Collections.Generic;
using Parabox.CSG;
using UnityEngine;

public class CuttingScript : MonoBehaviour
{
    public GameObject fish;
    
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformCut()
    {
        if (fish != null)
        {
            var m = CSG.Subtract(fish, gameObject);
            
            fish.GetComponent<MeshFilter>().sharedMesh = m.mesh;
            fish.GetComponent<MeshRenderer>().sharedMaterials = m.materials.ToArray();

            Destroy(fish.GetComponent<Collider>());
            fish.AddComponent<MeshCollider>().convex = true;

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
