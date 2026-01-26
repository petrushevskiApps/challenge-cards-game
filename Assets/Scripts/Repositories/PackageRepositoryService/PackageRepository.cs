using System;
using System.Collections.Generic;
using System.IO;
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
            var packageDtos = _packages.ToDtoList();
            
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            
            string json = JsonConvert.SerializeObject(packageDtos, settings);
            File.WriteAllText(SaveFilePath, json);
            
            Debug.Log($"Packages saved successfully. Count: {packageDtos.Count}, Path: {SaveFilePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to save packages: {ex.Message}");
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
        SavePackages();
    }

    private void OnPackageTitleChanged(string title)
    {
        SavePackages();
    }
}

