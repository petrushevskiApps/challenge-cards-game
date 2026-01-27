using System;
using System.Collections.Generic;

namespace PetrushevskiApps.WhosGame.Scripts.Models
{
    [Serializable]
    public class RandomChallengesData
        : IRandomChallengesData
    {
        public List<RandomChallengeModel> RandomChallenges  { get; set; }
    }
}
