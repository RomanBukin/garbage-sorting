using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Animation
{
    public class CornerIconAnimation : FadeAnimation
    {
        public float fadeScale; 
        
        [Header("Transition")]
        [Tooltip("Debug (Readonly)")]
        public Vector2 size;
        public Vector2 outPosition;
        public float duration = 1f;
        public Ease ease = Ease.Linear;

        private RectTransform _rectTransform;
        private bool _canFade;
        private bool _needFadeIn;

        private Vector2 _defaultMinAnchor;
        private Vector2 _defaultMaxAnchor;
        private Vector2 _outMinAnchor;
        private Vector2 _outMaxAnchor;
        private Vector2 _centerMinAnchor;
        private Vector2 _centerMaxAnchor;
        private Tweener _moveMaxAnchorTween;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            Graphic = GetComponent<Graphic>();

            if (_rectTransform == null)
            {
                enabled = false;
                return;
            }

            _canFade = Graphic != null;

            _defaultMinAnchor = _rectTransform.anchorMin;
            _defaultMaxAnchor = _rectTransform.anchorMax;

            size = new Vector2(
                _defaultMaxAnchor.x - _defaultMinAnchor.x,
                _defaultMaxAnchor.y - _defaultMinAnchor.y);
            var halfSize = size / 2f;

            _centerMinAnchor = Vector2.one * 0.5f - halfSize;
            _centerMaxAnchor = Vector2.one * 0.5f + halfSize;
            _outMinAnchor = outPosition - halfSize;
            _outMaxAnchor = outPosition + halfSize;

            _rectTransform.anchorMin = _outMinAnchor;
            _rectTransform.anchorMax = _outMaxAnchor;
        }

        private void OnDestroy()
        {
            if (_moveMaxAnchorTween?.active == true)
            {
                DOTween.Kill(_rectTransform);
            }
        }

        public void MoveOut(TweenCallback onComplete = null)
        {
            MoveTo(_outMinAnchor, _outMaxAnchor, onComplete);
        }

        public void MoveToDefault(TweenCallback onComplete = null)
        {
            MoveTo(_defaultMinAnchor, _defaultMaxAnchor, onComplete);
        }

        public void MoveToCenter(TweenCallback onComplete = null)
        {
            print("Move to center");
            MoveTo(_centerMinAnchor, _centerMaxAnchor, onComplete);
        }

        private void MoveTo(Vector2 minAnchor, Vector2 maxAnchor, TweenCallback onComplete)
        {
            if (_moveMaxAnchorTween?.active == true)
            {
                print("Stop MoveTo");
                DOTween.Kill(_rectTransform);
            }
            
            _rectTransform
                .DOAnchorMin(minAnchor, duration)
                .SetEase(ease);
            _moveMaxAnchorTween = _rectTransform
                .DOAnchorMax(maxAnchor, duration)
                .SetEase(ease)
                .OnComplete(onComplete);
        }

        public void FadeOut(TweenCallback onComplete = null)
        {
            if (!_canFade)
            {
                return;
            }

            print("Fade out");
            Graphic.CrossFadeAlpha(-0.5f, fadeOutDuration, false);
            transform
                .DOScale(Vector3.one * fadeScale, fadeOutDuration)
                .OnComplete(onComplete);
            _needFadeIn = true;
        }
        
        public void FadeIn(TweenCallback onComplete = null)
        {
            if (!_canFade)
            {
                return;
            }

            if (!_needFadeIn)
            {
                onComplete?.Invoke();
                return;
            }

            _needFadeIn = false;

            print("Fade in");
            Graphic.CrossFadeAlpha(1f, fadeInDuration, false);
            transform
                .DOScale(Vector3.one, fadeInDuration)
                .OnComplete(onComplete);
        }
    }
}