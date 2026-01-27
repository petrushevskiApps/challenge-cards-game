using System.Collections.Generic;
using System.Linq;
using TwoOneTwoGames.UIManager.InfiniteScrollList;
using TwoOneTwoGames.UIManager.ScreenNavigation;
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
    
    public void Setup(IPackageModel packageModel, InfiniteScrollController infiniteScrollController)
    {
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
        _cards.Add(card);
        SetScrollController();
    }

    private void OnCardsAdded(List<IChallengeCardModel> challenges)
    {
        _cards.AddRange(challenges);
        SetScrollController();
    }

    private void OnCardRemoved(IChallengeCardModel card)
    {
        _cards.Remove(card);
        SetScrollController();
    }

    private void SetScrollController()
    {
        _infiniteScrollController.Clear();
        _infiniteScrollController.AddPage(_cards.Count);
    }
    
}