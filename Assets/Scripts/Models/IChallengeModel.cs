using System;

namespace PetrushevskiApps.WhosGame.Scripts.Models
{
    public interface IChallengeModel
    {
        event Action<string> TitleChanged;
        event Action<string> DescriptionChanged;
        event Action<bool> SelectionChanged;
        string Id { get; }
        string Title { get; }
        string Description { get; }
        bool IsSelected { get; }
        void UpdateTitle(string title);
        void UpdateDescription(string description);
        void SetSelected(bool selected);
    }
}