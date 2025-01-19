using UnityEngine;

namespace GameCore.Navigation
{
    [CreateAssetMenu(fileName = "LoadingData", menuName = "Game/LoadingData")]
    public class LoadingData : ScriptableObject
    {
        public float minimumLoadingTime = 2f;
    }
}