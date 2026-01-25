using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SwitchView : MonoBehaviour
{
    [SerializeField]
    private Image _checkmark;

    [SerializeField]
    private Image _checkmarkBackground;

    [SerializeField]
    private Color _toggleOnBackgroundColor;
    
    private Toggle _toggle;
    private RectTransform _rectTransform;

    public void UpdateToggleState(bool isOn)
    {
        OnValueChanged(isOn);
    }
    
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _rectTransform = _checkmark.GetComponent<RectTransform>();
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
            SetAnchorMinAndMax(1, -10);
            _checkmarkBackground.color = _toggleOnBackgroundColor;
        }
        else
        {
            SetAnchorMinAndMax(0, 10);
            _checkmarkBackground.color = Color.white;
        }
    }

    private void SetAnchorMinAndMax(float value, float anchoredPos)
    {
        var min = _rectTransform.anchorMin;
        min.x = value;
        _rectTransform.anchorMin = min;
        
        var max = _rectTransform.anchorMax;
        max.x = value;
        _rectTransform.anchorMax = max;

        var anchoredPosition = _rectTransform.anchoredPosition;
        anchoredPosition.x = anchoredPos;
        _rectTransform.anchoredPosition = anchoredPosition;
    }
}
