using DG.Tweening;
using UnityEngine;

namespace Animation
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MenuPanelAnimation : MonoBehaviour
    {
        public float margin;
        public float duration;
        public Vector2 minStartSize;
        public Vector2 maxStartSize;

        private RectTransform _rectTransform;
        private Vector2 _startMin;
        private Vector2 _startMax;
        private CanvasGroup _canvasGroup;
        private Sequence _sequence;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();

            if (_rectTransform == null)
            {
                enabled = false;
            }

            _canvasGroup.alpha = 0f;
        }

        public void Show(Vector2 startPosition)
        {
            print("Panel show");
            var startSizeX = maxStartSize.x - minStartSize.x > 0.01f
                ? Random.Range(minStartSize.x, maxStartSize.x)
                : minStartSize.x;
            
            var startSizeY = maxStartSize.y - minStartSize.y > 0.01f
                ? Random.Range(minStartSize.y, maxStartSize.y)
                : minStartSize.y;
            
            var halfSize = new Vector2(startSizeX, startSizeY) / 2f;
            _startMin = startPosition - halfSize;
            _startMax = startPosition + halfSize;
            var minTo = Vector2.one * margin;
            var maxTo = Vector2.one * (1f - margin);

            _rectTransform.anchorMin = _startMin;
            _rectTransform.anchorMax = _startMax;

            _sequence = DOTween.Sequence();
            _sequence
                .Append(_rectTransform.DOAnchorMin(minTo, duration))
                .Join(_rectTransform.DOAnchorMax(maxTo, duration))
                .PlayForward();
            
            transform.SetAsLastSibling();
            _canvasGroup.DOFade(1f, duration);
        }

        public void Hide()
        {
            _sequence = DOTween.Sequence();
            _sequence
                .Append(_rectTransform.DOAnchorMin(_startMin, duration))
                .Join(_rectTransform.DOAnchorMax(_startMax, duration))
                .PlayForward();
            
            transform.SetAsFirstSibling();
            _canvasGroup.DOFade(0f, duration);
        }
    }
}