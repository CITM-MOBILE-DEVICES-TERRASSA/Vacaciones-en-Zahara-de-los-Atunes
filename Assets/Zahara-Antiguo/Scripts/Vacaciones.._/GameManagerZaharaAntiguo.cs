using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerZaharaAntiguo : MonoBehaviour
{
    #region Singleton
    private static GameManagerZaharaAntiguo _instance;
    public static GameManagerZaharaAntiguo Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    #endregion
}
