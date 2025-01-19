using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameCore.Navigation
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private LoadingData loadingData;
        private static SceneLoader instance;

        public static SceneLoader Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("SceneLoader");
                    instance = go.AddComponent<SceneLoader>();
                    DontDestroyOnLoad(go);
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Cargar el MainMenu a través de la loading screen al inicio
            if (SceneManager.GetActiveScene().name != SceneNames.LoadingScreen)
            {
                LoadSceneWithLoading(SceneNames.MainMenu);
            }
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void LoadSceneWithLoading(string targetSceneName)
        {
            StartCoroutine(LoadSceneWithLoadingRoutine(targetSceneName));
        }

        private IEnumerator LoadSceneWithLoadingRoutine(string targetSceneName)
        {
            // Cargar la pantalla de loading
            AsyncOperation loadingScreenOperation = SceneManager.LoadSceneAsync(SceneNames.LoadingScreen);
            while (!loadingScreenOperation.isDone)
            {
                yield return null;
            }

            // Esperar el tiempo mínimo de loading
            yield return new WaitForSeconds(loadingData.minimumLoadingTime);

            // Cargar la escena objetivo
            AsyncOperation targetSceneOperation = SceneManager.LoadSceneAsync(targetSceneName);
            targetSceneOperation.allowSceneActivation = false;

            while (targetSceneOperation.progress < 0.9f)
            {
                yield return null;
            }

            targetSceneOperation.allowSceneActivation = true;
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
}