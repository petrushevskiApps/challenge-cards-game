using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public interface IPackageRepository
{
    event Action<IPackageModel> PackageAdded;
    event Action<IPackageModel> PackageRemoved;
    event Action PackagesChanged;
    IReadOnlyList<IPackageModel> Packages { get; }
    bool IsLoaded { get; }
    IPackageModel CreatePackage(string title);
    bool DeletePackage(IPackageModel package);
    UniTask SavePackagesAsync();
    UniTask LoadPackagesAsync();
}
