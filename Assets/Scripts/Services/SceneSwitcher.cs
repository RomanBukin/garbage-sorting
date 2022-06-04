using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class FadeEventArgs : EventArgs
    {
        public float TargetValue;
    }

    public class SceneSwitcher : MonoBehaviour
    {
        public EventHandler<FadeEventArgs> FadeFinished;

        public float durationOnStart = 1f;
        public float duration = 0.3f;

        private GameObject _canvasGo;
        private CanvasGroup _canvasGroup;
        private AsyncOperation _operation;

        private void Awake()
        {
            _canvasGo = GetComponentInChildren<Canvas>().gameObject;
            _canvasGroup = GetComponentInChildren<CanvasGroup>();

            if (_canvasGo == null || _canvasGroup == null)
            {
                Debug.LogWarning("No Canvas or CanvasGroup");
            }
        }

        private void Start()
        {
            StartCoroutine(FadeLoadingScreen(0f, durationOnStart));
        }

        public void SwitchScene(string sceneName)
        {
            StartCoroutine(SwitchTask(sceneName, duration, duration));
        }

        public void PrepareScene(string sceneName)
        {
            _operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            _operation.allowSceneActivation = false;
        }

        public void SwitchToPreparedScene(float delay)
        {
            if (_operation == null)
            {
                Debug.LogWarning("There is no prepared scene");
            }
            
            StartCoroutine(SwitchTask(duration, duration, delay));
        }

        private IEnumerator SwitchTask(string sceneName, float outDuration, float inDuration)
        {
            PrepareScene(sceneName);
            yield return SwitchTask(outDuration, inDuration);
        }

        private IEnumerator SwitchTask(float outDuration, float inDuration, float delay = 0f)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }
            
            yield return FadeLoadingScreen(1f, outDuration);

            _operation.allowSceneActivation = true;
            while (!_operation.isDone)
            {
                yield return null;
            }
            _operation = null;

            var prevActive = SceneManager.GetActiveScene().name;
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
            SceneManager.UnloadSceneAsync(prevActive);

            yield return FadeLoadingScreen(0f, inDuration);
        }

        private IEnumerator FadeLoadingScreen(float targetValue, float seconds)
        {
            if (targetValue > 0f)
            {
                _canvasGo.SetActive(true);
            }

            float startValue = _canvasGroup.alpha;
            float time = 0;

            while (time < seconds)
            {
                _canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / seconds);
                time += Time.deltaTime;
                yield return null;
            }

            _canvasGroup.alpha = targetValue;

            if (targetValue == 0f)
            {
                _canvasGo.SetActive(false);
            }

            OnFadeFinished();
        }

        private void OnFadeFinished()
        {
            var args = new FadeEventArgs
            {
                TargetValue = _canvasGroup.alpha
            };

            FadeFinished?.Invoke(this, args);
        }
    }
}