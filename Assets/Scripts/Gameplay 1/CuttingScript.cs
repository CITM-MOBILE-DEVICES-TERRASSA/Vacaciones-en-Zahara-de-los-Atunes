using System;
using System.Collections;
using System.Collections.Generic;
using Parabox.CSG;
using UnityEngine;

public class CuttingScript : MonoBehaviour
{
    public GameObject fish;
    private ScoreManagerLevel scoremanagerlevel;
    [Min(1)]public int maxScorePerCut = 200;
    // Relacion inversamente proporcional de la distancia
    [Range(0.01f,10.0f)]public float distanceFactor = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
           scoremanagerlevel = FindObjectOfType<ScoreManagerLevel>();
    }

    public void PerformCut()
    {
        // Convertir a corutina y añadir un delay para sincronizar con la animacion?
        
        if (fish == null) return;
        
        // Realizamos la operacion de corte
        var m = CSG.Subtract(fish, gameObject);
    
        fish.GetComponent<MeshFilter>().sharedMesh = m.mesh;
        fish.GetComponent<MeshRenderer>().sharedMaterials = m.materials.ToArray();
    
        // Recalcula la colision del objeto cortado
        Destroy(fish.GetComponent<Collider>());
        fish.AddComponent<MeshCollider>().convex = true;
        fish.transform.position = Vector3.zero;

        var marker = fish.GetComponent<PositionMark>().instance.transform;

        if (marker != null)
        {
            // Calculamos la distancia en el eje X
            var distanceX = Mathf.Abs(transform.position.x - marker.position.x);
        
            // Calculamos la puntuación proporcional a la distancia absoluta
            var cutScore = Mathf.RoundToInt(maxScorePerCut/(1+(distanceX*distanceFactor)));
            scoremanagerlevel.UpdateScoreLevel1(cutScore);
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
