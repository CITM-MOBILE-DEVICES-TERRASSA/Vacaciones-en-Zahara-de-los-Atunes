using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGame1 : MonoBehaviour
{
    public Timer timer;
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void Pause()
    {
        gameObject.SetActive(true);
        timer.isPaused = true;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        timer.isPaused = false;
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        timer.isPaused = false;
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Lobby()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene("Lobby");
    }
    public void GameSelection()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameSelection");
    }
    public void FridgeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Collectibles");
    }
}

