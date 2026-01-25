using System.Collections.Generic;

public interface IRandomChallengeRepository
{
    List<string> GetRandomChallenges(int count, string language);
}
