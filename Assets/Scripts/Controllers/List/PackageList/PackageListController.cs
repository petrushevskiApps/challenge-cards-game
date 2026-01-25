using System.Collections.Generic;
using System.Linq;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UserInterface.Views;

public class PackageListController: IPackageListController
{
    // Internal
    private IListView _listView;
    private readonly Dictionary<string, PackageListItemView> _activeViews = new();
    
    // Injected
    private readonly IScreenNavigation _screenNavigation;
    private readonly IPackageRepository _packageRepository;
    private readonly PackageListItemView.Pool _packagesPool;
    
    public PackageListController(
        IScreenNavigation screenNavigation,
        IPackageRepository packageRepository,
        PackageListItemView.Pool packagesPool)
    {
        _screenNavigation = screenNavigation;
        _packageRepository = packageRepository;
        _packagesPool = packagesPool;
    }
    
    public void Setup(IListView view, IReadOnlyList<IPackageModel> packages)
    {
        _listView = view;

        SubscribeToPackageEvents();
        
        foreach (IPackageModel package in packages)
        {
            AddItemView(package);
        }
    }

    public void Clear()
    {
        UnsubscribeFromPackageEvents();
        List<string> activeViewsKeys = _activeViews.Keys.ToList();
        foreach (string packageModel in activeViewsKeys)
        {
            RemoveItem(packageModel);
        }
        _activeViews.Clear();
    }

    private void AddItemView(IPackageModel package)
    {
        if (_activeViews.ContainsKey(package.Id))
        {
            Debug.LogWarning($"View for package {package} already exists.");
            return;
        }
        PackageListItemView view = _packagesPool.Spawn(_listView.ContentContainer);
        view.Setup(new PackageItemViewController(_screenNavigation, package, view));
        _activeViews.Add(package.Id, view);
    }

    private void RemoveItem(string packageId)
    {
        if (!_activeViews.TryGetValue(packageId, out PackageListItemView view))
        {
            Debug.LogWarning($"Tried to remove view for package {packageId} but none exists.");
            return;
        }

        view.Cleanup();
        _packagesPool.Despawn(view);
        _activeViews.Remove(packageId);
    }
    
    private void SubscribeToPackageEvents()
    {
        if (_packageRepository != null)
        {
            _packageRepository.PackageAdded += OnCardAdded;
            _packageRepository.PackageRemoved += OnCardRemoved;
        }
    }

    private void UnsubscribeFromPackageEvents()
    {
        if (_packageRepository != null)
        {
            _packageRepository.PackageAdded -= OnCardAdded;
            _packageRepository.PackageRemoved -= OnCardRemoved;
        }
    }

    private void OnCardAdded(IPackageModel package)
    {
        AddItemView(package);
    }

    private void OnCardRemoved(IPackageModel package)
    {
        RemoveItem(package.Id);
    }
}