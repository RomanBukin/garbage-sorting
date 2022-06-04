using System;
using UnityEngine;

namespace Components
{
    public class UIButtonEventArgs : EventArgs
    {
        public bool IsChecked;
    }
    
    [RequireComponent(typeof(BoxCollider2D))]
    public class UIButton : MonoBehaviour
    {
        public event Action<EventArgs> ButtonDown;
        public event Action<EventArgs> ButtonUp; 

        public bool isToggle;
        public bool isChecked;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (isToggle)
            {
                isChecked = !isChecked;
            }
            else
            {
                isChecked = true;
            }
            OnButtonDown();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!isToggle)
            {
                isChecked = false;
            }
            OnButtonUp();
        }

        private void OnButtonDown()
        {
            var args = new UIButtonEventArgs()
            {
                IsChecked = isChecked
            };
            ButtonDown?.Invoke(args);
        }

        private void OnButtonUp()
        {
            var args = new UIButtonEventArgs()
            {
                IsChecked = isChecked
            };
            ButtonUp?.Invoke(args);
        }
    }
}
