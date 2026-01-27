using PetrushevskiApps.WhosGame.Scripts.Models;
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace PetrushevskiApps.WhosGame.Scripts.Repositories.PackageRepositoryService
{
    public interface IPackageRepository
{
    event Action<IPackageModel> PackageAdded;
    event Action<IPackageModel> PackageRemoved;
    event Action PackagesChanged;
    bool IsLoaded { get; }
    IReadOnlyList<IPackageModel> Packages { get; }
    IPackageModel CreatePackage(string title);
    bool DeletePackage(IPackageModel package);
    UniTask SavePackagesAsync();
    UniTask LoadPackagesAsync();
}
}
