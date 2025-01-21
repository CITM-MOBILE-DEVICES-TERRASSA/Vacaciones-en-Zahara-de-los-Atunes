using UnityEngine;
using UnityEngine.UI;
using GameCore.Navigation;

namespace GameCore.Navigation
{
    public class BackNavigationController : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Button backButton;  // Este campo aparecerá en el Inspector

        private ISceneLoader sceneLoader;
        private string currentScene;
        private string targetScene;

        private void Start()
        {
            // Obtener la referencia al SceneLoader
            sceneLoader = SceneLoader.Instance;

            if (backButton == null)
            {
                Debug.LogError("Back button reference is missing! Please assign it in the inspector.", this);
                return;
            }

            currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            SetupBackNavigation();
            backButton.onClick.AddListener(HandleBackNavigation);
        }

        private void SetupBackNavigation()
        {
            switch (currentScene)
            {
                case SceneNames.Lobby:
                    targetScene = SceneNames.MainMenu;
                    break;
                case SceneNames.GameSelection:
                    targetScene = SceneNames.Lobby;
                    break;
                case SceneNames.Collectibles:
                    targetScene = SceneNames.MainMenu;
                    break;
                default:
                    Debug.LogWarning($"Back navigation not configured for scene: {currentScene}", this);
                    backButton.gameObject.SetActive(false);
                    return;
            }
        }

        private void HandleBackNavigation()
        {
            if (!string.IsNullOrEmpty(targetScene))
            {
                SceneLoader.Instance.LoadScene(targetScene);
            }
        }

        private void OnDestroy()
        {
            if (backButton != null)
            {
                backButton.onClick.RemoveListener(HandleBackNavigation);
            }
        }
    }
}