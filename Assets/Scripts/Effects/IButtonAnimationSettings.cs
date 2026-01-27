using PrimeTween;

public interface IButtonAnimationSettings
{
    float SquishScaleY { get; }
    float SquishScaleX { get; }
    float SquishDuration { get; }
    float BounceDuration { get; }
    float OvershootStrength { get; }
    Ease Ease { get; }
    float DebounceDelay { get; }
}