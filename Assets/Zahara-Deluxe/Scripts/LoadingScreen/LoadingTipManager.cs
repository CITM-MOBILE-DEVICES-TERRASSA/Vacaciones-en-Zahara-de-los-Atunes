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

        private void OnEnable()
        {
            DisplayNewTip();
        }

        public void DisplayNewTip()
        {
            if (tipText == null || tipsData == null) return;

            tipText.alpha = 0;

            string newTip = tipsData.GetRandomTip();
            tipText.text = newTip;

            tipText.DOFade(1f, fadeInDuration);
        }
    }
}