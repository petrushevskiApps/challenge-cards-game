using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class PackageModel : IPackageModel
{
    public event Action<IChallengeCardModel> CardAdded;
    public event Action<List<IChallengeCardModel>> CardsAdded;
    public event Action<IChallengeCardModel> CardRemoved;
    public event Action CardsNumberChanged;
    public event Action<string> TitleChanged;

    private readonly List<ChallengeCardModel> _challengeCards = new();

    public string Id { get; init; }
    public string Title { get; private set; }
    public IReadOnlyList<IChallengeCardModel> ChallengeCards => _challengeCards;

    public PackageModel()
    {
        Id = Guid.NewGuid().ToString();
    }

    public PackageModel(string title) : this()
    {
        Title = title;
    }
    
    public bool AddChallengeCardModel(IChallengeCardModel card)
    {
        if (card is not ChallengeCardModel challengeCard)
        {
            return false;
        }

        _challengeCards.Add(challengeCard);
        CardAdded?.Invoke(card);
        CardsNumberChanged?.Invoke();
        return true;
    }

    public bool AddChallengeModelsInBulk(List<IChallengeCardModel> cards)
    {
        _challengeCards.AddRange(cards.Cast<ChallengeCardModel>());
        CardsAdded?.Invoke(cards);
        CardsNumberChanged?.Invoke();
        return true;
    }
    
    public bool RemoveChallengeCardModel(IChallengeCardModel card)
    {
        if (card is not ChallengeCardModel challengeCard)
        {
            return false;
        }
        
        if (!_challengeCards.Remove(challengeCard))
        {
            return false;
        }

        CardRemoved?.Invoke(card);
        CardsNumberChanged?.Invoke();
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

    public int GetNumberOfActiveCards()
    {
        return _challengeCards.Count(card => card.IsSelected);
    }
}