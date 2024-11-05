using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtons : MonoBehaviour
{
    public GameObject playButton;

    public void PlayMinigameOne()
    {
        SceneLoader.Instance.LoadMeta();
    }

}
