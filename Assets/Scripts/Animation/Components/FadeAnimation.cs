using UnityEngine;
using UnityEngine.UI;

namespace Animation
{
    [RequireComponent(typeof(Graphic))]
    public class FadeAnimation : MonoBehaviour
    {
        [Header("Fade")]
        public float fadeInDuration = 1f;
        public float fadeOutDuration = 0.5f;
        
        protected Graphic Graphic;
        
        private void Awake()
        {
            Graphic = GetComponent<Graphic>();
            Graphic.CrossFadeAlpha(0f, 0f, false);
        }
        
        public void FadeOut(float duration = 0f)
        {
            var dur = duration > 0 ? duration : fadeOutDuration;
            Graphic.CrossFadeAlpha(0f, dur, false);
        }

        public void FadeIn(float duration = 0f)
        {
            var dur = duration > 0 ? duration : fadeInDuration;
            Graphic.CrossFadeAlpha(1f, dur, false);
        }
    
    }
}
