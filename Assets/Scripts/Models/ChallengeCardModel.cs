using System;

[Serializable]
public class ChallengeCardModel : IChallengeCardModel
{
    public event Action<string> TitleChanged;
    public event Action<string> DescriptionChanged;
    public event Action<bool> SelectionChanged;

    public string Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsSelected { get; set; }

    public ChallengeCardModel()
    {
        Id = Guid.NewGuid().ToString();
    }

    public ChallengeCardModel(string title, string description, bool isSelected = false): this()
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