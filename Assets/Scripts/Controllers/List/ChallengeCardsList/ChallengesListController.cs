using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using TwoOneTwoGames.UIManager.InfiniteScrollList;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UserInterface.Screens;
using UserInterface.Views;
using Zenject;

public class ChallengesListController : 
    IChallengesListController,
    IListDataSource
{
    // Internal
    private IPackageModel _packageModel;
    private InfiniteScrollController _infiniteScrollController;
    private List<IChallengeCardModel> _cards;
    private IChallengeScreenView _view;

    // Injected
    private readonly IPackageRepository _packageRepository;
    private readonly IPopupNavigation _popupNavigation;
    private readonly IItemViewPool _itemViewPool;

    public ChallengesListController(
        [Inject(Id = "ChallengeItemViewPool")]IItemViewPool itemViewPool,
        IPackageRepository packageRepository,
        IPopupNavigation popupNavigation)
    {
        _packageRepository = packageRepository;
        _popupNavigation = popupNavigation;
        _itemViewPool = itemViewPool;
        
    }
    
    public void Setup(
        IChallengeScreenView challengeScreenView, 
        IPackageModel packageModel,
        InfiniteScrollController infiniteScrollController)
    {
        _view = challengeScreenView;
        _packageModel = packageModel;
        _cards = _packageModel.ChallengeCards.ToList();
        
        if (_infiniteScrollController == null)
        {
            _infiniteScrollController = infiniteScrollController;
            _infiniteScrollController.Setup(this, _itemViewPool);
        }

        _infiniteScrollController.AddPage(_cards.Count);
        SubscribeToPackageEvents();
    }

    public void Clear()
    {
        UnsubscribeFromPackageEvents();
        _cards.Clear();
        _infiniteScrollController.Clear();
    }

    public void SetCards(IEnumerable<IChallengeCardModel> challengeCards)
    {
        _cards.Clear();
        _cards.AddRange(challengeCards);
        SetScrollController();
    }
    
    public void SetItemViewData(IItemView rowView)
    {
        ChallengeItemView view = rowView.View.GetComponent<ChallengeItemView>();
        view.Setup(_packageRepository, _cards[rowView.Index],_packageModel, _popupNavigation);
    }
    
    private void SubscribeToPackageEvents()
    {
        if (_packageModel != null)
        {
            _packageModel.CardAdded += OnCardAdded;
            _packageModel.CardsAdded += OnCardsAdded;
            _packageModel.CardRemoved += OnCardRemoved;
        }
    }

    private void UnsubscribeFromPackageEvents()
    {
        if (_packageModel != null)
        {
            _packageModel.CardAdded -= OnCardAdded;
            _packageModel.CardsAdded -= OnCardsAdded;
            _packageModel.CardRemoved -= OnCardRemoved;
        }
    }

    private void OnCardAdded(IChallengeCardModel card)
    {
        _infiniteScrollController.RowsAddedEvent += ScrollToBottom;
        _cards.Add(card);
        SetScrollController();
    }

    private void OnCardsAdded(List<IChallengeCardModel> challenges)
    {
        
        _infiniteScrollController.RowsAddedEvent += ScrollToBottom;
        _cards.AddRange(challenges);
        SetScrollController();
    }

    private void ScrollToBottom(object sender, EventArgs e)
    {
        // We need to wait for the list to reset before we can scroll
        // hence waiting for the Rows Added Event.
        _infiniteScrollController.RowsAddedEvent -= ScrollToBottom;
        _view.ScrollToBottom();
    }

    private int _scrollToElement;
    private void OnCardRemoved(IChallengeCardModel card)
    {
        _infiniteScrollController.RowsAddedEvent += ScrollToElement;
        _scrollToElement = _infiniteScrollController.GetFirstFullyVisibleItemView().Index;
        _cards.Remove(card);
        SetScrollController();
    }

    private void ScrollToElement(object sender, EventArgs eventArgs)
    {
        // We need to wait for the list to reset before we can scroll
        // hence waiting for the Rows Added Event.
        _infiniteScrollController.RowsAddedEvent -= ScrollToElement;
        _view.ScrollTo(_scrollToElement);
        _scrollToElement = 0;
    }
    
    private void SetScrollController()
    {
        _infiniteScrollController.Clear();
        _infiniteScrollController.AddPage(_cards.Count);
    }
    
}