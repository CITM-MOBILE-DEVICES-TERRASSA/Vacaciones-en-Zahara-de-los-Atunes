using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatusManager : MonoBehaviour
{
    public static GameStatusManager Instance { get; private set; }

    [Header("Game Status")]
    public bool hasWonGame1 = false;
    public bool hasWonGame2 = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }
}
