using System;
using System.Collections.Generic;

[Serializable]
public class PackageModel : IPackageModel
{
    public event Action<IChallengeCardModel> CardAdded;
    public event Action<IChallengeCardModel> CardRemoved;
    public event Action CardsNumberChanged;
    public event Action<string> TitleChanged;
    public string Id { get; }
    public string Title { get; set; }
    public List<ChallengeCardModel> ChallengeCards { get; set; } = new();
    
    public PackageModel()
    {
        Id = Guid.NewGuid().ToString();
    }

    public PackageModel(string title): this()
    {
        Title = title;
    }
    
    public bool AddChallengeCardModel(IChallengeCardModel card)
    {
        if (card is not ChallengeCardModel challengeCard)
        {
            return false;
        }

        ChallengeCards.Add(challengeCard);
        CardAdded?.Invoke(card);
        CardsNumberChanged?.Invoke();
        return true;
    }

    public bool RemoveChallengeCardModel(IChallengeCardModel card)
    {
        if (card is not ChallengeCardModel challengeCard)
        {
            return false;
        }
        
        if (!ChallengeCards.Remove(challengeCard))
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
}