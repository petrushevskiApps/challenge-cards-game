using System;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    /// <summary>
    /// Used to control global UI elements from screens.
    /// </summary>
    [Serializable]
    public class GlobalUiElement
    {
        [SerializeField]
        [Tooltip("GameObject that will be turned on or off when screen is shown or hidden")]
        private GameObject _element;

        [SerializeField]
        [Tooltip("If true will turn on the element when the screen is shown, when false the opposite is true")]
        private bool _onScreenShownElementState;

        public void SetElement(bool isActive)
        {
            _element.SetActive(_onScreenShownElementState == isActive);
        }
    }
}