using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PetrushevskiApps.WhosGame.Scripts.Views
{
    public class ToggleView : MonoBehaviour
    {
        [SerializeField]
        private Toggle _toggle;
        [SerializeField]
        private List<ImageColorData> _imageColors;
        [SerializeField]
        private List<TextColorData> _textColors;
        
        public Toggle Toggle => _toggle;
        
        public void UpdateToggleState(bool isOn)
        {
            _toggle.SetIsOnWithoutNotify(isOn);
            OnValueChanged(isOn);
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
                foreach (ImageColorData data in _imageColors)
                {
                    data.Graphic.color = data.ColorOn;
                }
                foreach (TextColorData data in _textColors)
                {
                    data.Text.color = data.ColorOn;
                }
            }
            else
            {
                foreach (ImageColorData data in _imageColors)
                {
                    data.Graphic.color = data.ColorOff;
                }
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
    
    [Serializable]
    public class ImageColorData
    {
        [SerializeField]
        public Image Graphic;
        [SerializeField]
        public Color ColorOn;
        [SerializeField]
        public Color ColorOff;
    }
}