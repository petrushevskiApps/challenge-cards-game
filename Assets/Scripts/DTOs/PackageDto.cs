using System;
using System.Collections.Generic;

namespace DTOs
{
    [Serializable]
    public class PackageDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<ChallengeCardDto> ChallengeCards { get; set; } = new();

        public PackageDto() { }

        public PackageDto(string id, string title, List<ChallengeCardDto> challengeCards)
        {
            Id = id;
            Title = title;
            ChallengeCards = challengeCards ?? new List<ChallengeCardDto>();
        }
    }
}
