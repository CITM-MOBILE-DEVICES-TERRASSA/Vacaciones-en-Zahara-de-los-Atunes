using UnityEngine;

namespace LoadingSystem.Data
{
    [CreateAssetMenu(fileName = "LoadingRotationData", menuName = "Loading/Rotation Data")]
    public class LoadingRotationData : ScriptableObject
    {
        [SerializeField] private float rotationSpeed = 180f;
        [Tooltip("Duration of a complete rotation in seconds")]
        [SerializeField] private float tweenDuration = 1f;

        public float RotationSpeed => rotationSpeed;
        public float TweenDuration => tweenDuration;

        public float GetTargetRotation()
        {
            return rotationSpeed > 0 ? 360f : -360f;
        }
    }
}