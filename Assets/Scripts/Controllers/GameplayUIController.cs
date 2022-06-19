using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class GameplayUIController : MonoBehaviour
    {
        [SerializeField] private float messageTime = 1f;

        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI correctCountText;
        [SerializeField] private TextMeshProUGUI incorrectCountText;
        [SerializeField] private TextMeshProUGUI missedCountText;
        [SerializeField] private TextMeshProUGUI correctMessageText;
        [SerializeField] private TextMeshProUGUI incorrectMessageText;
        [SerializeField] private TextMeshProUGUI missedMessageText;
        [SerializeField] private TextMeshProUGUI gameOverText;

        // private DateTime _startTime;
        private bool _timerStarted;
            

        public void Reset()
        {
            timerText.text = "00:00";
            SetCorrectCount(0);
            SetIncorrectCount(0);
            SetMissedCount(0);

            correctMessageText.enabled = false;
            incorrectMessageText.enabled = false;
            missedMessageText.enabled = false;
            gameOverText.enabled = false;
        }

        public void StartTimer(DateTime startTime)
        {
            // _startTime = startTime;
            _timerStarted = true;
            StartCoroutine(UpdateTimerTask(startTime));
        }

        public void StopTimer()
        {
            _timerStarted = false;
        }

        private IEnumerator UpdateTimerTask(DateTime startTime)
        {
            TimeSpan timeSpan = DateTime.Now - startTime;
            TimeSpan delta = TimeSpan.FromSeconds(1);
            var wait = new WaitForSeconds(1);
            
            while (_timerStarted)
            {
                timeSpan = timeSpan.Add(delta);
                var format = timeSpan.Hours > 0 ? "hh\\:mm\\:ss" : "mm\\:ss";
                timerText.text = timeSpan.ToString(format);
                yield return wait;
            }
        }

        public void SetCorrectCount(int value)
        {
            correctCountText.text = value.ToString();
        }

        public void SetIncorrectCount(int value)
        {
            incorrectCountText.text = value.ToString();
        }

        public void SetMissedCount(int value)
        {
            missedCountText.text = value.ToString();
        }

        public void ShowCorrect()
        {
            StartCoroutine(ShowMessageTask(correctMessageText));
        }

        public void ShowIncorrect()
        {
            StartCoroutine(ShowMessageTask(incorrectMessageText));
        }

        public void ShowMissed()
        {
            StartCoroutine(ShowMessageTask(missedMessageText));
        }

        public void ShowGameOver()
        {
            gameOverText.enabled = true;
        }

        private IEnumerator ShowMessageTask(Behaviour behaviour)
        {
            behaviour.enabled = true;
            yield return new WaitForSeconds(messageTime);

            // ReSharper disable once Unity.InefficientPropertyAccess
            behaviour.enabled = false;
        }
    }
}