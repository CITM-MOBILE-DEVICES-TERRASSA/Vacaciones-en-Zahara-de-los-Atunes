using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    ScoreL1 score;
    // Start is called before the first frame update
    void Start()
    {
        score = FindObjectOfType<ScoreL1>();
        gameObject.SetActive(false);
    }
    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
        score.score = 0;
    }
    public void Lobby()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
        score.score = 0;
    }
}

