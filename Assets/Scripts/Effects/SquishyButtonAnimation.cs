using PetrushevskiApps.WhosGame.Scripts.Effects;
using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;
using Zenject;

namespace PetrushevskiApps.WhosGame.Scripts.Effects
{
    public class SquishyButtonAnimation
        : MonoBehaviour,
            IPointerDownHandler,
            IPointerUpHandler
{
    [SerializeField]
    private ButtonAnimationSettings _overrideSettings;

    private Vector3 _originalScale;

    [Inject]
    private IButtonAnimationSettings _settings;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        if (_overrideSettings != null)
        {
            _settings = _overrideSettings;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_settings == null)
        {
            return;
        }

        Tween.StopAll(transform);

        Tween.ScaleY(transform, _originalScale.y * _settings.SquishScaleY, _settings.SquishDuration, _settings.Ease,
            useUnscaledTime: true);
        Tween.ScaleX(transform, _originalScale.x * _settings.SquishScaleX, _settings.SquishDuration, _settings.Ease,
            useUnscaledTime: true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_settings == null)
        {
            return;
        }

        Tween.StopAll(transform);

        Tween.ScaleY(transform, _originalScale.y, _settings.BounceDuration,
            Easing.Overshoot(_settings.OvershootStrength), useUnscaledTime: true);
        Tween.ScaleX(transform, _originalScale.x, _settings.BounceDuration,
            Easing.Overshoot(_settings.OvershootStrength), useUnscaledTime: true);
    }
    }
}