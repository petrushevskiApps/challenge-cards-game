using UnityEngine;
using UnityEngine.EventSystems;
using PrimeTween;
using Zenject;

public class JellyButtonAnimation
    : MonoBehaviour,
        IPointerDownHandler
{
    [SerializeField]
    private ButtonAnimationSettings _overrideSettings;

    private Vector3 _originalScale;
    private Sequence _currentSequence;

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

        _currentSequence.Stop();

        _currentSequence = Sequence.Create(useUnscaledTime: true)
            .Group(Tween.ScaleY(transform, _originalScale.y * _settings.SquishScaleY, _settings.SquishDuration,
                _settings.Ease))
            .Group(Tween.ScaleX(transform, _originalScale.x * _settings.SquishScaleX, _settings.SquishDuration,
                _settings.Ease))
            .ChainDelay(_settings.DebounceDelay)
            .Group(Tween.ScaleY(transform, _originalScale.y, _settings.BounceDuration,
                Easing.Overshoot(_settings.OvershootStrength)))
            .Group(Tween.ScaleX(transform, _originalScale.x, _settings.BounceDuration,
                Easing.Overshoot(_settings.OvershootStrength)));
    }
}