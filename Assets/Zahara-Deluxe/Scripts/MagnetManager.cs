using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetManager : MonoBehaviour
{
    [Header("References")]
    public GameObject magnetGame1Prefab;
    public GameObject magnetGame2Prefab;

    private void Start()
    {
        Debug.Log($"GameStatusManager.Instance.hasWonGame1: {GameStatusManager.Instance.hasWonGame1}");
        Debug.Log($"GameStatusManager.Instance.hasWonGame2: {GameStatusManager.Instance.hasWonGame2}");

        if (GameStatusManager.Instance.hasWonGame1)
        {
            magnetGame1Prefab.SetActive(true); 
            Debug.Log("Magnet 1 Activated");
        }
        else
        {
            magnetGame1Prefab.SetActive(false); 
            Debug.Log("Magnet 1 Deactivated");
        }

        if (GameStatusManager.Instance.hasWonGame2)
        {
            magnetGame2Prefab.SetActive(true); 
            Debug.Log("Magnet 2 Activated");
        }
        else
        {
            magnetGame2Prefab.SetActive(false); 
            Debug.Log("Magnet 2 Deactivated");
        }
    }
}
