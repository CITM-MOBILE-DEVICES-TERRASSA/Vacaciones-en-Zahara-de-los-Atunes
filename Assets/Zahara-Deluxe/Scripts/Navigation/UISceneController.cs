using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GameCore.Navigation
{
    public class UISceneController : MonoBehaviour
    {
        [SerializeField] private SceneTransitionData[] sceneTransitions;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Start()
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.InOutSine);
        }

        public void NavigateToScene(string sceneName)
        {
            canvasGroup.DOFade(0f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => {
                SceneLoader.Instance.LoadScene(sceneName);
            });
        }

        public void QuitGame()
        {
            canvasGroup.DOFade(0f, 0.5f).SetEase(Ease.InOutSine).OnComplete(() => {
                SceneLoader.Instance.QuitGame();
            });
        }
    }
}