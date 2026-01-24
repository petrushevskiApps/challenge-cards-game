using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class PackageRepository : IPackageRepository
{
    private const string SAVE_FILE_NAME = "packages";
    
    public event Action<IPackageModel> PackageAdded;
    public event Action<IPackageModel> PackageRemoved;
    public event Action PackagesChanged;
    
    private readonly List<IPackageModel> _packages = new();
    private string SaveFilePath => Path.Combine(Application.persistentDataPath, $"{SAVE_FILE_NAME}.json");

    public IReadOnlyList<IPackageModel> Packages => _packages;

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
        SavePackages();
        
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
        SavePackages();
        
        return true;
    }

    public void SavePackages()
    {
        try
        {
            string json = JsonConvert.SerializeObject(_packages);
            File.WriteAllText(SaveFilePath, json);
            Debug.Log($"Packages saved to: {SaveFilePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save packages: {ex}");
        }
    }

    public void LoadPackages()
    {
        try
        {
            if (!File.Exists(SaveFilePath))
            {
                Debug.Log("No saved packages found. Starting fresh.");
                return;
            }

            string json = File.ReadAllText(SaveFilePath);
            List<PackageModel> saveData = JsonConvert.DeserializeObject<List<PackageModel>>(json);

            if (saveData == null)
            {
                Debug.LogWarning("Save file exists but contains no packages.");
                return;
            }

            _packages.Clear();

            foreach (var package in saveData)
            {
                _packages.Add(package);
                SubscribeToPackageEvents(package);
            }

            Debug.Log($"Loaded {_packages.Count} packages from: {SaveFilePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load packages: {ex}");
        }
    }
    
    private void SubscribeToPackageEvents(IPackageModel package)
    {
        package.CardAdded += OnCardUpdated;
        package.CardRemoved += OnCardUpdated;
    }

    private void UnsubscribeFromPackageEvents(IPackageModel package)
    {
        package.CardAdded -= OnCardUpdated;
        package.CardRemoved -= OnCardUpdated;
    }

    private void OnCardUpdated(IChallengeCardModel card)
    {
        SavePackages();
    }
}
