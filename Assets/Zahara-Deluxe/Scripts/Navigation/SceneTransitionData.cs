using UnityEngine;

namespace GameCore.Navigation
{
    [CreateAssetMenu(fileName = "SceneTransitionData", menuName = "Game/SceneTransitionData")]
    public class SceneTransitionData : ScriptableObject
    {
        public string targetScene;
        public float transitionDuration = 0.5f;
        public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    }
}