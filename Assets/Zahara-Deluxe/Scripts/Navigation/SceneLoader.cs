using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace GameCore.Navigation
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private LoadingData loadingData;
        private static SceneLoader instance;
        private bool isLoading = false;
        private LoadingData defaultLoadingData;
        private bool isFirstLoad = true;

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
            InitializeDefaultLoadingData();

            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene != SceneNames.LoadingScreen && isFirstLoad && SceneManager.GetActiveScene().buildIndex == 0)
            {
                LoadSceneWithLoading(SceneNames.MainMenu);
            }
            isFirstLoad = false;
        }

        private void InitializeDefaultLoadingData()
        {
            if (loadingData == null)
            {
                defaultLoadingData = ScriptableObject.CreateInstance<LoadingData>();
                defaultLoadingData.minimumLoadingTime = 2f;
                loadingData = defaultLoadingData;
            }
        }

        public void LoadScene(string sceneName)
        {
            if (!isLoading)
            {
                SceneManager.LoadSceneAsync(sceneName);
            }
        }

        public void LoadSceneWithLoading(string targetSceneName)
        {
            if (!isLoading)
            {
                StartCoroutine(LoadSceneWithLoadingRoutine(targetSceneName));
            }
        }

        private IEnumerator LoadSceneWithLoadingRoutine(string targetSceneName)
        {
            isLoading = true;

            if (loadingData == null)
            {
                InitializeDefaultLoadingData();
            }

            try
            {
                AsyncOperation loadingScreenOperation = SceneManager.LoadSceneAsync(SceneNames.LoadingScreen);
                while (!loadingScreenOperation.isDone)
                {
                    yield return null;
                }

                float elapsedTime = 0f;
                while (elapsedTime < loadingData.minimumLoadingTime)
                {
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                AsyncOperation targetSceneOperation = SceneManager.LoadSceneAsync(targetSceneName);
                targetSceneOperation.allowSceneActivation = false;

                while (targetSceneOperation.progress < 0.9f)
                {
                    yield return null;
                }

                targetSceneOperation.allowSceneActivation = true;
                while (!targetSceneOperation.isDone)
                {
                    yield return null;
                }
            }
            finally
            {
                isLoading = false;
            }
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnDestroy()
        {
            if (defaultLoadingData != null)
            {
                Destroy(defaultLoadingData);
            }
        }
    }
}