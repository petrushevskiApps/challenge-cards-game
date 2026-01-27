using System;

namespace PetrushevskiApps.WhosGame.Scripts.Models
{
    [Serializable]
    public class ChallengeModel : IChallengeModel
{
    public event Action<string> TitleChanged;
    public event Action<string> DescriptionChanged;
    public event Action<bool> SelectionChanged;

    public string Id { get; init; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsSelected { get; private set; }

    public ChallengeModel()
    {
        Id = Guid.NewGuid().ToString();
    }

    public ChallengeModel(string title, string description, bool isSelected = false) : this()
    {
        UpdateTitle(title);
        UpdateDescription(description);
        SetSelectedInternal(isSelected);
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be null, empty, or whitespace.", nameof(title));
        }

        if (Title == title)
        {
            return;
        }

        Title = title;
        TitleChanged?.Invoke(title);
    }

    public void UpdateDescription(string description)
    {
        if (Description == description)
        {
            return;
        }

        Description = description;
        DescriptionChanged?.Invoke(description);
    }

    public void SetSelected(bool selected)
    {
        if (IsSelected == selected)
        {
            return;
        }

        SetSelectedInternal(selected);
        SelectionChanged?.Invoke(selected);
    }

    private void SetSelectedInternal(bool selected)
    {
        IsSelected = selected;
    }
    }
}