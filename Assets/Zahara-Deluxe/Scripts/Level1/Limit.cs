using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Limit : MonoBehaviour
{
    public GameObject Limite;

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
