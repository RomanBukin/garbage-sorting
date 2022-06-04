using System;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleBackgroundHider : MonoBehaviour
    {
        private Toggle _toggle;

        private void Start()
        {
            _toggle.targetGraphic.enabled = !_toggle.isOn;
        }

        private void OnEnable()
        {
            _toggle = GetComponent<Toggle>();

            _toggle.targetGraphic.enabled = !_toggle.isOn;
            _toggle.onValueChanged.AddListener(OnValueChanged); 
        }

        private void OnDisable()
        {
            _toggle.targetGraphic.enabled = true;
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        private void OnValueChanged(bool isChecked)
        {
            _toggle.targetGraphic.enabled = !_toggle.isOn;
        }
    }
}
