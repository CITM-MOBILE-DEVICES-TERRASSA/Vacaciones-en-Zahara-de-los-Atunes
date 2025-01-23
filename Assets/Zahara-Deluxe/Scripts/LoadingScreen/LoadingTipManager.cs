using UnityEngine;
using TMPro;
using LoadingSystem.Data;
using LoadingSystem.Interfaces;
using DG.Tweening;

namespace LoadingSystem
{
    public class LoadingTipManager : MonoBehaviour, ITipDisplay
    {
        [SerializeField] private LoadingTipsData tipsData;
        [SerializeField] private TextMeshProUGUI tipText;
        [SerializeField] private float fadeInDuration = 0.5f;
        [SerializeField] private float fadeOutDuration = 0.3f; 

        private bool isTransitioning = false; 

        private void OnEnable()
        {
            DisplayNewTip();
        }

        private void Update()
        {
            
            if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !isTransitioning)
            {
                TransitionToNewTip();
            }
        }

        private void TransitionToNewTip()
        {
            if (tipText == null || tipsData == null) return;

            isTransitioning = true;

            
            tipText.DOFade(0f, fadeOutDuration)
                .OnComplete(() => {
                    
                    string newTip = tipsData.GetRandomTip();
                    tipText.text = newTip;

                    tipText.DOFade(1f, fadeInDuration)
                        .OnComplete(() => {
                            isTransitioning = false; 
                        });
                });
        }

        public void DisplayNewTip()
        {
            if (tipText == null || tipsData == null) return;

            tipText.alpha = 0;
            string newTip = tipsData.GetRandomTip();
            tipText.text = newTip;
            tipText.DOFade(1f, fadeInDuration)
                .OnComplete(() => {
                    isTransitioning = false;
                });
        }

        private void OnDisable()
        {
            
            tipText.DOKill();
        }
    }
}
