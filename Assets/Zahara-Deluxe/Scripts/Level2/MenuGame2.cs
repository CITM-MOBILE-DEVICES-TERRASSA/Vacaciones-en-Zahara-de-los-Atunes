using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGame2 : MonoBehaviour
{
    private Game2Manager gameManager;
    public Button pauseButton;

    void Start()
    {
        gameManager = Game2Manager.Instance;
        gameObject.SetActive(false);
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(Pause);
        }
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        AudioManager.Instance.PauseGameAudio();
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance.ResumeGameAudio();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        AudioManager.Instance.ResumeGameAudio();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
    }

    public void Lobby()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        AudioManager.Instance.ResumeGameAudio();
        SceneManager.LoadScene("Lobby");
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        AudioManager.Instance.ResumeGameAudio();
    }
}