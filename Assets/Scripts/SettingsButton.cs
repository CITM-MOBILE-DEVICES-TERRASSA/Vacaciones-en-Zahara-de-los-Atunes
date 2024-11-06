using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsCanvas;

    private void Start()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
        }
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true); 
    }

    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneLoader.Instance.LoadMainLobby();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
