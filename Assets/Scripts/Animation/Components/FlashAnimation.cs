using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Animation.Components
{
    [RequireComponent(typeof(Graphic))]
    public class FlashAnimation : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private float scale;
        [SerializeField] private Color color;
        [SerializeField] private bool refresh;

        private Graphic _graphic;
        private Sequence _sequence;

        private void Awake()
        {
            _graphic = GetComponent<Graphic>();
        }

        public void Play()
        {
            if (refresh || _sequence == null)
            {
                _sequence = DOTween.Sequence()
                    .Append(transform.DOScale(Vector3.one * scale, duration))
                    .Join(_graphic.DOColor(color, duration))
                    .SetEase(Ease.Flash)
                    .SetLoops(2, LoopType.Yoyo);
            }
            
            _sequence.PlayForward();
        }
    }
}
