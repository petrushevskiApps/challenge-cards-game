using System.Collections.Generic;

namespace PetrushevskiApps.WhosGame.Scripts.Repositories.ChallengeRepositoryService
{
    public interface IRandomChallengeRepository
    {
        List<string> GetRandomChallenges(int count, string language);
    }
}
