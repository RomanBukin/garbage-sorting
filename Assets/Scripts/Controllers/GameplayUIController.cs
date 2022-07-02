using System;
using System.Collections;
using Animation.Components;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class GameplayUIController : MonoBehaviour
    {
        [SerializeField] private float messageTime = 1f;

        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI correctCountText;
        [SerializeField] private TextMeshProUGUI incorrectCountText;
        [SerializeField] private TextMeshProUGUI missedCountText;
        
        [SerializeField] private GameObject correctMessage;
        [SerializeField] private GameObject incorrectMessage;
        [SerializeField] private GameObject missedMessage;

        [SerializeField] private FlashAnimation correctIconFlash;
        [SerializeField] private FlashAnimation incorrectIconFlash;
        [SerializeField] private FlashAnimation missedIconFlash;
        
        [Header("GameOver window elements")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject placePanel;
        [SerializeField] private GameObject placeRecordPanel;
        [SerializeField] private GameObject incorrectPanel;
        [SerializeField] private GameObject missedPanel;
        [SerializeField] private Text placeText; 
        [SerializeField] private Text timeText; 
        [SerializeField] private Text correctText; 
        [SerializeField] private Text incorrectText; 
        [SerializeField] private Text missedText;

        private bool _timerStarted;
            

        public void Reset()
        {
            timerText.text = "";
            correctCountText.text = "0";
            incorrectCountText.text = "0";
            missedCountText.text = "0";

            correctMessage.SetActive(false);
            incorrectMessage.SetActive(false);
            missedMessage.SetActive(false);
            gameOverPanel.SetActive(false);
        }

        public void StartTimer(DateTime startTime)
        {
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
            correctIconFlash.Play();
        }

        public void SetIncorrectCount(int value)
        {
            incorrectCountText.text = value.ToString();
            incorrectIconFlash.Play();
        }

        public void SetMissedCount(int value)
        {
            missedCountText.text = value.ToString();
            missedIconFlash.Play();
        }

        public void ShowCorrect()
        {
            StartCoroutine(ShowMessageTask(correctMessage));
        }

        public void ShowIncorrect()
        {
            StartCoroutine(ShowMessageTask(incorrectMessage));
        }

        public void ShowMissed()
        {
            StartCoroutine(ShowMessageTask(missedMessage));
        }

        public void ShowGameOver(Record record, GameType type, int position)
        {
            if (position == 1)
            {
                placeRecordPanel.SetActive(true);
                placePanel.SetActive(false);
            }
            else
            {
                placeText.text = position.ToString();
                placeRecordPanel.SetActive(false);
                placePanel.SetActive(true);
            }
            
            if (type == GameType.Classic)
            {
                incorrectText.text = record.incorrect.ToString();
                missedText.text = record.missed.ToString();
                incorrectPanel.SetActive(true);
                missedPanel.SetActive(true);
            }
            else
            {
                incorrectPanel.SetActive(false);
                missedPanel.SetActive(false);
            }

            
            var format = record.time.Hours > 0 ? "hh\\:mm\\:ss\\.f" : "mm\\:ss\\.f";
            timeText.text = record.time.ToString(format);
            correctText.text = record.correct.ToString();
            
            gameOverPanel.SetActive(true);
        }

        private IEnumerator ShowMessageTask(GameObject go)
        {
            go.SetActive(true);
            yield return new WaitForSeconds(messageTime);

            // ReSharper disable once Unity.InefficientPropertyAccess
            go.SetActive(false);
        }
    }
}