using JetBrains.Annotations;
using PrimeTween;
using UnityEngine;

[CreateAssetMenu(
    fileName = "ButtonAnimationSettings", 
    menuName = "Configuration/Button Animation Settings")]
public class ButtonAnimationSettings : ScriptableObject, IButtonAnimationSettings
{
    [field: Header("Squish Settings")]
    [field: SerializeField] 
    public float SquishScaleY {get; [UsedImplicitly] private set;}
    [field: SerializeField] 
    public float SquishScaleX {get; [UsedImplicitly] private set;}

    [field: SerializeField] 
    public float SquishDuration {get; [UsedImplicitly] private set;}

    [field: SerializeField] 
    public Ease Ease {get; [UsedImplicitly] private set;}
    [field: SerializeField] 
    public float DebounceDelay { get; [UsedImplicitly] private set;}

    [field: Header("Bounce Back Settings")]
    [field: SerializeField] 
    public float BounceDuration {get; [UsedImplicitly] private set;}

    [field: SerializeField] 
    public float OvershootStrength {get; [UsedImplicitly] private set;}
}