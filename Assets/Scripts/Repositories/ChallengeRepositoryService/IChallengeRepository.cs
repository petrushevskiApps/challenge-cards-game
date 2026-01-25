using System.Collections.Generic;

public interface IChallengeRepository
{
    List<string> GetRandomChallenges(int count, string language);
}
