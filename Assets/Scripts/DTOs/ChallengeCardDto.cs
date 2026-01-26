using System;

namespace DTOs
{
    [Serializable]
    public class ChallengeCardDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsSelected { get; set; }

        public ChallengeCardDto() { }

        public ChallengeCardDto(string id, string title, string description, bool isSelected)
        {
            Id = id;
            Title = title;
            Description = description;
            IsSelected = isSelected;
        }
    }
}
