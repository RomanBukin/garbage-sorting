using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RecordItem : MonoBehaviour
    {
        [SerializeField] private Text correctText;
        [SerializeField] private Text incorrectText;
        [SerializeField] private Text missedText;
        [SerializeField] private Text timeText;
        
        public void SetValues(int correct, TimeSpan time)
        {
            var format = time.Hours > 0 ? "hh\\:mm\\:ss\\.f" : "mm\\:ss\\.f";
            
            correctText.text = correct.ToString();
            timeText.text = time.ToString(format);
        }

        public void SetValues(int correct, int incorrect, int missed, TimeSpan time)
        {
            SetValues(correct, time);

            incorrectText.text = incorrect.ToString();
            missedText.text = missed.ToString();
        }
    }
}
