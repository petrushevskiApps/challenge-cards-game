using TMPro;
using TwoOneTwoGames.UIManager.InfiniteScrollList;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UserInterface.Views;
using Zenject;

namespace UserInterface.Screens
{
    public class ChallengeScreen : UIScreen, IChallengeScreenView
    {
        [SerializeField]
        private TextMeshProUGUI _packageTitle;
        [SerializeField]
        private Button _backButton; 
        [SerializeField]
        private Button _editTitleButton;
        [SerializeField]
        private Button _deletePackageButton;
        [SerializeField]
        private Button _createCustomChallengeButton;
        [SerializeField]
        private TextMeshProUGUI _customChallengeButtonLabel;
        [SerializeField]
        private Button _createRandomChallengeButton;
        [SerializeField]
        private TextMeshProUGUI _randomChallengeButtonLabel;
        [SerializeField]
        private SwitchView _selectAllCardsToggle;
        [SerializeField]
        private TMP_InputField _searchInput;
        [SerializeField]
        private ListView _listView;

        [SerializeField]
        private TextMeshProUGUI _selectAllLabel;

        [SerializeField]
        private TextMeshProUGUI _searchInputLabel;
        [SerializeField]
        private InfiniteScrollController _scrollController;
        
        public IListView ListView => _listView;
        public InfiniteScrollController InfiniteListScrollController => _scrollController;

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
            _selectAllCardsToggle.Toggle.onValueChanged.AddListener(_controller.SelectAllCardsToggled);
            _searchInput.onValueChanged.AddListener(InputFieldValueChanged);
            base.Show(navArguments);
            _controller.ScreenShown();
        }

        public override void Hide()
        {
            base.Hide();
            _controller.ScreenHidden();
            _selectAllCardsToggle.UpdateToggleState(false);
            _searchInput.text = string.Empty;
        }

        public override void Close()
        {
            base.Close();
            _backButton.onClick.RemoveListener(_controller.BackClicked);
            _editTitleButton.onClick.RemoveListener(_controller.EditTitleClicked);
            _deletePackageButton.onClick.RemoveListener(_controller.DeletePackageClicked);
            _createCustomChallengeButton.onClick.RemoveListener(_controller.CreateCustomChallengeClicked);
            _createRandomChallengeButton.onClick.RemoveListener(_controller.CreateRandomChallengeClicked);
            _selectAllCardsToggle.Toggle.onValueChanged.RemoveListener(_controller.SelectAllCardsToggled);
            _searchInput.onValueChanged.RemoveListener(InputFieldValueChanged);
        }

        public void SetPackageTitle(string title)
        {
            _packageTitle.text = title;
        }
        public void SetSelectAllLabel(string label)
        {
            _selectAllLabel.text = label;
        }
        public void SetSearchInputLabel(string label)
        {
            _searchInputLabel.text = label;
        }
        public void SetCustomChallengeButtonLabel(string label)
        {
            _customChallengeButtonLabel.text = label;
        }
        public void SetRandomChallengeButtonLabel(string label)
        {
            _randomChallengeButtonLabel.text = label;
        }
        private void InputFieldValueChanged(string text)
        {
            _controller.SearchInputChanged(text, this);
        }
    }
}