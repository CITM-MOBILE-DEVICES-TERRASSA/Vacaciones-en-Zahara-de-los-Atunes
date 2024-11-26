using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtons : MonoBehaviour
{
    public GameObject levelButton;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayLevelOne()
    {
        audioSource.Play();
        SceneLoader.Instance.LoadGameplay();
    }
    public void PlayLevel2()
    {
        audioSource.Play();
        SceneLoader.Instance.LoadScene("Level 2");
    }

    public void ReturnToLobby()
    {
        audioSource.Play();
        SceneLoader.Instance.LoadMainLobby();
    }

}
