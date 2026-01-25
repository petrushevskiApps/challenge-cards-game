using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UserInterface.Views;
using Zenject;

namespace UserInterface.Screens
{
    public struct ChallengeScreenNavigationArguments
    {
        public IPackageModel PackageModel { get; }

        public ChallengeScreenNavigationArguments(IPackageModel packageModel)
        {
            PackageModel = packageModel;
        }
    }
    public class ChallengeScreen : UIScreen, IChallengeScreenView
    {
        [SerializeField]
        private Button _backButton; 
        [SerializeField]
        private Button _editTitleButton;
        [SerializeField]
        private Button _deletePackageButton;
        [SerializeField]
        private Button _createCustomChallengeButton;
        [SerializeField]
        private Button _createRandomChallengeButton;
        [SerializeField]
        private Toggle _selectAllCardsToggle;
        [SerializeField]
        private InputField _searchInput;
        [SerializeField]
        private ListView _listView;

        public IListView ListView => _listView;

        private IChallengeScreenController _controller;

        [Inject]
        public void Initialize(IChallengeScreenController challengeScreenController)
        {
            _controller = challengeScreenController;
        }

        public override void Show<TArguments>(TArguments navArguments)
        {
            if (navArguments is ChallengeScreenNavigationArguments arguments)
            {
                _controller.Setup(this, arguments.PackageModel);
            }
            _backButton.onClick.AddListener(_controller.BackClicked);
            _editTitleButton.onClick.AddListener(_controller.EditTitleClicked); 
            _deletePackageButton.onClick.AddListener(_controller.DeletePackageClicked);
            _createCustomChallengeButton.onClick.AddListener(_controller.CreateCustomChallengeClicked);
            _createRandomChallengeButton.onClick.AddListener(_controller.CreateRandomChallengeClicked);
            _selectAllCardsToggle.onValueChanged.AddListener(_controller.SelectAllCardsToggled);
            // _searchInput.onValueChanged.AddListener(_controller.SearchInputChanged);
            base.Show(navArguments);
        }

        public override void Resume()
        {
            base.Resume();
            _controller.ScreenResumed();
        }

        public override void Hide()
        {
            base.Hide();
            _controller.ScreenHidden();
        }

        public override void Close()
        {
            base.Close();
            _backButton.onClick.RemoveListener(_controller.BackClicked);
            _editTitleButton.onClick.RemoveListener(_controller.EditTitleClicked);
            _deletePackageButton.onClick.RemoveListener(_controller.DeletePackageClicked);
            _createCustomChallengeButton.onClick.RemoveListener(_controller.CreateCustomChallengeClicked);
            _createRandomChallengeButton.onClick.RemoveListener(_controller.CreateRandomChallengeClicked);
            _selectAllCardsToggle.onValueChanged.RemoveListener(_controller.SelectAllCardsToggled);
            // _searchInput.onValueChanged.RemoveListener(_controller.SearchInputChanged);
        }
    }
}