using System;
using DG.Tweening;
using UnityEngine;

namespace Animation
{
    public class Transition : MonoBehaviour
    {
        public Vector3 offset;
        public bool isReversed;
        public float duration;
        public Ease ease = Ease.Linear;

        private Vector3 _startPosition;
        private Vector3 _endPosition;

        private void Start()
        {
            _startPosition = transform.localPosition;
            _endPosition = _startPosition + offset;

            if (isReversed)
            {
                transform.localPosition = _endPosition;
            }
        }

        public Tweener Move(TweenCallback onComplete = null)
        {
            var positionTo = isReversed ? _startPosition : _endPosition;
            isReversed = !isReversed;

            return transform
                .DOLocalMove(positionTo, duration)
                .SetEase(ease)
                .OnComplete(onComplete);
        }
    }
}