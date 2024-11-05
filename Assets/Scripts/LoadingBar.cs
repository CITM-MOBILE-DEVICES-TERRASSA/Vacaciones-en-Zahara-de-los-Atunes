using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingBar : MonoBehaviour
{
    public Image loadingBarFill;  // Reference to the Image UI component you created for the fill
    public Text progressText;     // Optional: Reference to the Text UI component

    private void Start()
    {
        StartCoroutine(LoadGameAsync());
    }

    IEnumerator LoadGameAsync()
    {
        // Replace with your actual loading logic
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SplashScreen");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Get the loading progress (values are from 0 to 1)
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progress; // Set the fill amount of the image

            // Optional: Display the loading percentage as text
            if (progressText != null)
                progressText.text = Mathf.RoundToInt(progress * 100) + "%";

            // When loading is complete, activate the scene
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
