using UnityEngine;

namespace LoadingSystem.Data
{
    [CreateAssetMenu(fileName = "LoadingTipsData", menuName = "Loading/Tips Data")]
    public class LoadingTipsData : ScriptableObject
    {
        [SerializeField, TextArea(2, 5)]
        private string[] gameTips = new string[10];

        public string GetRandomTip()
        {
            if (gameTips == null || gameTips.Length == 0)
                return "¡Welcome to the game!";

            return gameTips[Random.Range(0, gameTips.Length)];
        }
    }
}