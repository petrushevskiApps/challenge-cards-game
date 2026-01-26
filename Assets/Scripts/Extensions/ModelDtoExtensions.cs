using System.Collections.Generic;
using System.Linq;
using DTOs;

public static class ModelDtoExtensions
{
    public static ChallengeCardDto ToDto(this IChallengeCardModel model)
    {
        if (model == null)
        {
            return null;
        }

        return new ChallengeCardDto(
            model.Id,
            model.Title,
            model.Description,
            model.IsSelected
        );
    }

    public static ChallengeCardModel ToModel(this ChallengeCardDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        return new ChallengeCardModel(dto.Title, dto.Description, dto.IsSelected)
        {
            Id = dto.Id
        };
    }

    public static PackageDto ToDto(this IPackageModel model)
    {
        if (model == null)
        {
            return null;
        }

        var cardDtos = model.ChallengeCards?
            .Select(card => card.ToDto())
            .ToList() ?? new List<ChallengeCardDto>();

        return new PackageDto(model.Id, model.Title, cardDtos);
    }

    public static PackageModel ToModel(this PackageDto dto)
    {
        if (dto == null)
        {
            return null;
        }

        var package = new PackageModel(dto.Title)
        {
            Id = dto.Id
        };

        if (dto.ChallengeCards != null)
        {
            foreach (var cardDto in dto.ChallengeCards)
            {
                var card = cardDto.ToModel();
                if (card != null)
                {
                    package.AddChallengeCardModel(card);
                }
            }
        }

        return package;
    }

    public static List<PackageDto> ToDtoList(this IEnumerable<IPackageModel> packages)
    {
        return packages?.Select(m => m.ToDto()).Where(dto => dto != null).ToList() ?? new List<PackageDto>();
    }

    public static List<PackageModel> ToModelList(this IEnumerable<PackageDto> dtos)
    {
        return dtos?.Select(dto => dto.ToModel()).Where(model => model != null).ToList() ?? new List<PackageModel>();
    }
}
