using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Views
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleView : MonoBehaviour
    {
        [Header("Background")]
        [SerializeField]
        private Image _background;
        [SerializeField]
        private Color _backgroundColorOn;
        [SerializeField]
        private Color _backgroundColorOff;

        [Header("Text")]
        [SerializeField]
        private List<TextColorData> _textColors;
        
        private Toggle _toggle;

        public Toggle Toggle => _toggle;
        
        public void UpdateToggleState(bool isOn)
        {
            _toggle.SetIsOnWithoutNotify(isOn);
            OnValueChanged(isOn);
        }
        
        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }
        
        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }
        
        private void OnValueChanged(bool isOn)
        {
            if (isOn)
            {
                _background.color = _backgroundColorOn;
                foreach (TextColorData data in _textColors)
                {
                    data.Text.color = data.ColorOn;
                }
            }
            else
            {
                _background.color = _backgroundColorOff;
                foreach (TextColorData data in _textColors)
                {
                    data.Text.color = data.ColorOff;
                }
            }
        }
    }

    [Serializable]
    public class TextColorData
    {
        [SerializeField]
        public TextMeshProUGUI Text;
        [SerializeField]
        public Color ColorOn;
        [SerializeField]
        public Color ColorOff;
    }
}