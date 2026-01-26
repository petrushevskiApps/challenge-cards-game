using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using DTOs;
using Newtonsoft.Json;
using UnityEngine;

public class PackageRepository : IPackageRepository
{
    private const string SAVE_FILE_NAME = "packages";
    
    public event Action<IPackageModel> PackageAdded;
    public event Action<IPackageModel> PackageRemoved;
    public event Action PackagesChanged;
    public IReadOnlyList<IPackageModel> Packages => _packages;
    public bool IsLoaded { get; private set; }
    
    private readonly List<IPackageModel> _packages = new();
    private string SaveFilePath => Path.Combine(Application.persistentDataPath, $"{SAVE_FILE_NAME}.json");

    public IPackageModel CreatePackage(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Package title cannot be null or empty", nameof(title));
        }

        var package = new PackageModel(title);
        _packages.Add(package);
        
        SubscribeToPackageEvents(package);
        
        PackageAdded?.Invoke(package);
        PackagesChanged?.Invoke();
        SavePackagesAsync().Forget();
        
        return package;
    }

    public bool DeletePackage(IPackageModel package)
    {
        if (package == null || !_packages.Contains(package))
        {
            return false;
        }

        UnsubscribeFromPackageEvents(package);
        _packages.Remove(package);
        
        PackageRemoved?.Invoke(package);
        PackagesChanged?.Invoke();
        SavePackagesAsync().Forget();
        
        return true;
    }

    public async UniTask SavePackagesAsync()
    {
        try
        {
            var packageDtos = _packages.ToDtoList();
            
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            
            string json = JsonConvert.SerializeObject(packageDtos, settings);
            await File.WriteAllTextAsync(SaveFilePath, json);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save packages: {ex.Message}");
        }
    }

    public async UniTask LoadPackagesAsync()
    {
        try
        {
            if (!File.Exists(SaveFilePath))
            {
                Debug.Log("No saved packages found. Starting fresh.");
                return;
            }

            string json = await File.ReadAllTextAsync(SaveFilePath);
            var packageDtos = JsonConvert.DeserializeObject<List<PackageDto>>(json);

            if (packageDtos == null)
            {
                Debug.LogWarning("Save file exists but contains no valid data.");
                return;
            }

            _packages.Clear();

            var loadedPackages = packageDtos.ToModelList();
            foreach (var package in loadedPackages)
            {
                _packages.Add(package);
                SubscribeToPackageEvents(package);
            }

            IsLoaded = true;
            Debug.Log($"Loaded {_packages.Count} packages from: {SaveFilePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load packages: {ex.Message}");
        }
    }

    private void SubscribeToPackageEvents(IPackageModel package)
    {
        package.CardAdded += OnCardUpdated;
        package.CardRemoved += OnCardUpdated;
        package.TitleChanged += OnPackageTitleChanged;
    }

    private void UnsubscribeFromPackageEvents(IPackageModel package)
    {
        package.CardAdded -= OnCardUpdated;
        package.CardRemoved -= OnCardUpdated;
        package.TitleChanged -= OnPackageTitleChanged;
    }

    private void OnCardUpdated(IChallengeCardModel card)
    {
        SavePackagesAsync().Forget();
    }

    private void OnPackageTitleChanged(string title)
    {
        SavePackagesAsync().Forget();
    }
}

