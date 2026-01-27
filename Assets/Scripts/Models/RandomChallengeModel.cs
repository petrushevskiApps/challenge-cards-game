using System;

namespace PetrushevskiApps.WhosGame.Scripts.Models
{
    [Serializable]
    public class RandomChallengeModel
        : IRandomChallengeModel
    {
        public string English { get; set; }
        public string Russian { get; set; }
    }
}
