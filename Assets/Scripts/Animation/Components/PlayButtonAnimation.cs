using DG.Tweening;
using UnityEngine;

namespace Animation.Components
{
    public class PlayButtonAnimation : FadeAnimation
    { 
        [Header("Pulsation")] 
        public float pulseDuration = 2f;
        public Vector3 pulseScale;
        public Color pulseColor;
        private Sequence _sequence;

        public void StartPulsation()
        {
            if (_sequence == null)
            {
                _sequence = DOTween.Sequence()
                    .Append(transform.DOScale(pulseScale, pulseDuration))
                    .Join(Graphic.DOColor(pulseColor, pulseDuration))
                    .Append(transform.DOScale(Vector3.one, pulseDuration))
                    .Join(Graphic.DOColor(Graphic.color, pulseDuration))
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Restart);
            }
            else
            {
                _sequence.Restart();
            }
        }

        public void PausePulsation()
        {
            _sequence?.Pause();
        }
    }
}