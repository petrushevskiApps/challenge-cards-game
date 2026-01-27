using PetrushevskiApps.WhosGame.Scripts.Views.List;
using UnityEngine.UI;

namespace PetrushevskiApps.WhosGame.Scripts.Views.Screens
{
    public interface IMainScreenView
    {
        IListView ListView { get; }
        ToggleGroup PackagesToggleGroup { get; }
        void SetMessage(string message);
        void SetPlayButton(bool isVisible);
        void SetPackageListLabel(string label);
        void SetCreatePackageButtonLabel(string label);
        void SetPlayButtonLabel(string label);
        void SetMessageGroupVisibility(bool isVisible);
    }
}