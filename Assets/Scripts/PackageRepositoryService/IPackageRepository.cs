using System;
using System.Collections.Generic;

public interface IPackageRepository
{
    event Action<IPackageModel> PackageAdded;
    event Action<IPackageModel> PackageRemoved;
    event Action PackagesChanged;
    IReadOnlyList<IPackageModel> Packages { get; }
    IPackageModel CreatePackage(string title);
    bool DeletePackage(IPackageModel package);
    void SavePackages();
    void LoadPackages();
}
