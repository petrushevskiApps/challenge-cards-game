using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

namespace PetrushevskiApps.WhosGame.Scripts.Views
{
    [RequireComponent(typeof(Toggle))]
    public class SwitchView : MonoBehaviour
{
    [SerializeField]
    private Toggle _toggle;
    [SerializeField]
    private Image _checkmark;
    [SerializeField]
    private Image _checkmarkBackground;
    [SerializeField]
    private Color _toggleOnBackgroundColor;
    [SerializeField]
    private float _animationDuration = 0.3f;
    [SerializeField]
    private float _scaleMultiplier = 1.3f;
    
    private RectTransform _rectTransform;
    private Sequence _currentAnimation;

    public Toggle Toggle => _toggle;
    
    public void UpdateToggleState(bool isOn)
    {
        _toggle.SetIsOnWithoutNotify(isOn);
        OnValueChanged(isOn);
    }
    
    private void Awake()
    {
        _toggle ??= GetComponent<Toggle>();
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
        _currentAnimation.Stop();

        float targetAnchor = isOn ? 1f : 0f;
        float targetPosition = isOn ? -10f : 10f;
        Color targetColor = isOn ? _toggleOnBackgroundColor : Color.white;

        AnimateSwitching(isOn, targetAnchor, targetPosition, targetColor);
    }

    private void AnimateSwitching(bool isOn, float targetAnchor, float targetPosition, Color targetColor)
    {
        _currentAnimation = Sequence.Create()
            .Group(Tween.Custom(
                _rectTransform.anchorMin.x,
                targetAnchor,
                _animationDuration,
                onValueChange: value => SetAnchorMinAndMax(value, Mathf.Lerp(
                    isOn ? 10f : -10f,
                    targetPosition,
                    (value - (isOn ? 0f : 1f)) / (targetAnchor - (isOn ? 0f : 1f))
                )),
                ease: Ease.OutCubic
            ))
            .Group(Tween.Color(
                _checkmarkBackground,
                targetColor,
                _animationDuration,
                ease: Ease.OutCubic
            ))
            .Group(Tween.Scale(
                _checkmark.transform,
                Vector3.one * _scaleMultiplier,
                _animationDuration / 2f,
                ease: Ease.OutQuad
            ).OnComplete(() =>
            {
                Tween.Scale(
                    _checkmark.transform,
                    Vector3.one,
                    _animationDuration / 2f,
                    ease: Ease.InQuad
                );
            }));
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
}
