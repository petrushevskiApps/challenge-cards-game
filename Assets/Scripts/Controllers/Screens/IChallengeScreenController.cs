using PetrushevskiApps.WhosGame.Scripts.Models;
using PetrushevskiApps.WhosGame.Scripts.Views.Screens;
using UnityEngine;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.Screens
{
    public interface IChallengeScreenController
    {
        void Setup(IChallengeScreenView view, IPackageModel argumentsPackageModel);
        void ScreenShown();
        void ScreenHidden();
        void BackClicked();
        void DeletePackageClicked();
        void CreateCustomChallengeClicked();
        void CreateRandomChallengeClicked();
        void SelectAllCardsToggled(bool isOn);
        void SearchInputChanged(string searchText, MonoBehaviour view);
        void PackageTitleChanged(string text);
    }
}