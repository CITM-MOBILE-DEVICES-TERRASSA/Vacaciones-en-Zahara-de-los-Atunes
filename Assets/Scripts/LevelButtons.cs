using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtons : MonoBehaviour
{
    public GameObject levelButton;

    public void PlayLevelOne()
    {
        SceneLoader.Instance.LoadGameplay();
    }

    public void ReturnToLobby()
    {
        SceneLoader.Instance.LoadMainLobby();
    }

}
