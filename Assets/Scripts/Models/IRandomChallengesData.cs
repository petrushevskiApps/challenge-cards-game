using System.Collections.Generic;

namespace PetrushevskiApps.WhosGame.Scripts.Models
{
    public interface IRandomChallengesData
    {
        List<RandomChallengeModel> RandomChallenges { get; set; }
    }
}