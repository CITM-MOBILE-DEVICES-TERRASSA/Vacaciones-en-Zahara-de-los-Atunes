using UnityEngine;
using DG.Tweening;
using LoadingSystem.Data;
using LoadingSystem.Interfaces;

namespace LoadingSystem
{
    public class LoadingImageRotator : MonoBehaviour, IRotatable
    {
        [SerializeField] private LoadingRotationData rotationData;

        private Sequence rotationSequence;
        private bool isRotating;

        private void OnEnable()
        {
            StartRotation();
        }

        private void OnDisable()
        {
            StopRotation();
        }

        public void StartRotation()
        {
            if (isRotating) return;

            isRotating = true;
            rotationSequence = DOTween.Sequence();

            rotationSequence.Append(
                transform
                    .DORotate(
                        new Vector3(0, 0, rotationData.GetTargetRotation()),
                        rotationData.TweenDuration,
                        RotateMode.FastBeyond360
                    )
                    .SetEase(Ease.Linear)
            );

            rotationSequence.SetLoops(-1, LoopType.Restart);
        }

        public void StopRotation()
        {
            if (!isRotating) return;

            rotationSequence?.Kill();
            isRotating = false;
        }

        private void OnDestroy()
        {
            StopRotation();
        }
    }
}