using System;
using System.Collections.Generic;
using System.Linq;

namespace PetrushevskiApps.WhosGame.Scripts.Models
{
    [Serializable]
    public class PackageModel : IPackageModel
{
    public event Action<IChallengeModel> CardAdded;
    public event Action<List<IChallengeModel>> CardsAdded;
    public event Action<IChallengeModel> CardRemoved;
    public event Action CardsNumberChanged;
    public event Action<string> TitleChanged;

    private readonly List<ChallengeModel> _challengeCards = new();

    public string Id { get; init; }
    public string Title { get; private set; }
    public IReadOnlyList<IChallengeModel> ChallengeCards => _challengeCards;

    public PackageModel()
    {
        Id = Guid.NewGuid().ToString();
    }

    public PackageModel(string title) : this()
    {
        Title = title;
    }
    
    public bool AddChallengeCardModel(IChallengeModel card)
    {
        if (card is not ChallengeModel challengeCard)
        {
            return false;
        }

        _challengeCards.Add(challengeCard);
        CardAdded?.Invoke(card);
        CardsNumberChanged?.Invoke();
        return true;
    }

    public bool AddChallengeModelsInBulk(List<IChallengeModel> cards)
    {
        _challengeCards.AddRange(cards.Cast<ChallengeModel>());
        CardsAdded?.Invoke(cards);
        CardsNumberChanged?.Invoke();
        return true;
    }
    
    public bool RemoveChallengeCardModel(IChallengeModel card)
    {
        if (card is not ChallengeModel challengeCard)
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
}