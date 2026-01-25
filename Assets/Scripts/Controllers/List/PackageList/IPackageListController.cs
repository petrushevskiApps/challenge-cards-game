using System.Collections.Generic;
using UserInterface.Views;

public interface IPackageListController
{
    void Setup(IListView view, IReadOnlyList<IPackageModel> packages);
    void Clear();
}