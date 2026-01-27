using System;
using System.Collections.Generic;
using PetrushevskiApps.WhosGame.Scripts.Models;
using PetrushevskiApps.WhosGame.Scripts.Views.List;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.List.PackageList
{
    public interface IPackageListController
    {
        void Setup(IListView view, IReadOnlyList<IPackageModel> packages, Action<IPackageModel, bool> onPackageSelected);
        void Clear();
    }
}