using System;
using System.Collections.Generic;

public class PackageModel : IPackageModel
{
    public event Action<IChallengeCardModel> CardAdded;
    public event Action<IChallengeCardModel> CardRemoved;
    public event Action<string> TitleChanged;
    
    public string Title { get; private set; }
    public IReadOnlyList<IChallengeCardModel> ChallengeCards => _challengeCards;
    
    private readonly List<IChallengeCardModel> _challengeCards;

    public PackageModel(string title)
    {
        Title = title;
        _challengeCards = new List<IChallengeCardModel>();
    }

    public bool AddChallengeCardModel(IChallengeCardModel card)
    {
        if (card == null)
        {
            return false;
        }

        _challengeCards.Add(card);
        CardAdded?.Invoke(card);
        return true;
    }

    public bool RemoveChallengeCardModel(IChallengeCardModel card)
    {
        if (!_challengeCards.Remove(card))
        {
            return false;
        }

        CardRemoved?.Invoke(card);
        return true;
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is null, empty or whitespace", nameof(title));
        }
        if (Title == title)
        {
            return;
        }   
        Title = title;
        TitleChanged?.Invoke(title);
    }
}