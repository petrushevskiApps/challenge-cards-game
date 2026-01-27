using TMPro;
using TwoOneTwoGames.UIManager.InfiniteScrollList;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Screens
{
    public class ChallengeScreen : UIScreen, IChallengeScreenView
    {
        [Header("Header")]
        [SerializeField]
        private Button _backButton; 
        [SerializeField]
        private TMP_InputField _editTitle;
        [SerializeField]
        private Button _deletePackageButton;

        [Header("Toolbar")]
        [SerializeField]
        private SwitchView _selectAllCardsToggle;
        [SerializeField]
        private TextMeshProUGUI _selectAllLabel;
        [SerializeField]
        private TMP_InputField _searchInput;
        [SerializeField]
        private TextMeshProUGUI _searchInputLabel;

        [Header("Content")]
        [SerializeField]
        private InfiniteScrollList _listView;
        [SerializeField]
        private InfiniteScrollController _scrollController;

        [Header("Footer")]
        [SerializeField]
        private Button _createCustomChallengeButton;
        [SerializeField]
        private TextMeshProUGUI _customChallengeButtonLabel;
        [SerializeField]
        private Button _createRandomChallengeButton;
        [SerializeField]
        private TextMeshProUGUI _randomChallengeButtonLabel;

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
            _editTitle.onValueChanged.AddListener(_controller.PackageTitleChanged); 
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
            _editTitle.onValueChanged.RemoveListener(_controller.PackageTitleChanged); 
            _deletePackageButton.onClick.RemoveListener(_controller.DeletePackageClicked);
            _createCustomChallengeButton.onClick.RemoveListener(_controller.CreateCustomChallengeClicked);
            _createRandomChallengeButton.onClick.RemoveListener(_controller.CreateRandomChallengeClicked);
            _selectAllCardsToggle.Toggle.onValueChanged.RemoveListener(_controller.SelectAllCardsToggled);
            _searchInput.onValueChanged.RemoveListener(InputFieldValueChanged);
        }

        public void SetPackageTitle(string title)
        {
            _editTitle.SetTextWithoutNotify(title);
        }

        public void ScrollToBottom()
        {
            _listView.ToBottom();
        }

        public void ScrollTo(int elementIndex)
        {
            _listView.ToElement(elementIndex);
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