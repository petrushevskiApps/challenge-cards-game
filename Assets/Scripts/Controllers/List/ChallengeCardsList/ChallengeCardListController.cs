using System.Collections.Generic;
using System.Linq;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UserInterface.Views;

public class ChallengeCardListController : 
    IChallengeCardListController
{
    // Internal
    private IListView _listView;
    private IPackageModel _packageModel;
    private readonly Dictionary<string, ChallengeCardListItemView> _activeViews = new();
    
    // Injected
    private readonly IPackageRepository _packageRepository;
    private readonly IPopupNavigation _popupNavigation;
    private readonly ChallengeCardListItemView.Pool _itemViewPool;

    public ChallengeCardListController(
        IPackageRepository packageRepository,
        IPopupNavigation popupNavigation,
        ChallengeCardListItemView.Pool itemViewPool)
    {
        _packageRepository = packageRepository;
        _popupNavigation = popupNavigation;
        _itemViewPool = itemViewPool;
    }
    
    public void Setup(IListView listView, IPackageModel packageModel)
    {
        _listView = listView;
        _packageModel = packageModel;
        
        SubscribeToPackageEvents();
        
        foreach (ChallengeCardModel card in _packageModel.ChallengeCards)
        {
            AddItemView(card);
        }
    }

    public void Clear()
    {
        UnsubscribeFromPackageEvents();
        List<string> activeViewsKeys = _activeViews.Keys.ToList();
        foreach (string challengeCardId in activeViewsKeys)
        {
            RemoveItem(challengeCardId);
        }
        _activeViews.Clear();
    }

    private void AddItemView(IChallengeCardModel challengeCard)
    {
        if (_activeViews.ContainsKey(challengeCard.Id))
        {
            Debug.LogWarning($"View for package {challengeCard.Id} already exists.");
            return;
        }
        ChallengeCardListItemView view = _itemViewPool.Spawn(_listView.ContentContainer);
        view.Setup(new CardItemViewController(_packageRepository, challengeCard,_packageModel, view, _popupNavigation));
        _activeViews.Add(challengeCard.Id, view);
    }

    private void RemoveItem(string challengeCardId)
    {
        if (!_activeViews.TryGetValue(challengeCardId, out ChallengeCardListItemView view))
        {
            Debug.LogWarning($"Tried to remove view for package {challengeCardId} but none exists.");
            return;
        }

        view.Cleanup();
        _itemViewPool.Despawn(view);
        _activeViews.Remove(challengeCardId);
    }

    private void SubscribeToPackageEvents()
    {
        if (_packageModel != null)
        {
            _packageModel.CardAdded += OnCardAdded;
            _packageModel.CardRemoved += OnCardRemoved;
        }
    }

    private void UnsubscribeFromPackageEvents()
    {
        if (_packageModel != null)
        {
            _packageModel.CardAdded -= OnCardAdded;
            _packageModel.CardRemoved -= OnCardRemoved;
        }
    }

    private void OnCardAdded(IChallengeCardModel card)
    {
        AddItemView(card);
    }

    private void OnCardRemoved(IChallengeCardModel card)
    {
        RemoveItem(card.Id);
    }
}