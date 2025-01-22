using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGame1 : MonoBehaviour
{
    public Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void Pause()
    {
        gameObject.SetActive(true);
        timer.isPaused = true;
    }
    public void Resume()
    {
        timer.isPaused = false;
        gameObject.SetActive(false);
    }
    public void Restart()
    {
        timer.isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
    }
    public void Lobby()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Lobby");
    }
    public void GameSelection()
    {
        SceneManager.LoadScene("GameSelection");
    }
    public void FridgeScene()
    {
        SceneManager.LoadScene("Collectibles");
    }
}

