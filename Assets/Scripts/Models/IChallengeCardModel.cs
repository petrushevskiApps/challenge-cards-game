using System;

public interface IChallengeCardModel
{
    string Title { get; }
    string Description { get; }
    bool IsSelected { get; }
    event Action<string> TitleChanged;
    event Action<string> DescriptionChanged;
    event Action<bool> SelectionChanged;
    void UpdateTitle(string title);
    void UpdateDescription(string description);
    void SetSelected(bool selected);
}