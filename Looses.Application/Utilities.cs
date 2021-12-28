using Looses.Application.DTO;

namespace Looses.Application;

public static class Utilities
{
    public static LossReadDto AsDto(this Domain.Entities.Looses entity)
    {
        return new LossReadDto(
            entity.Id,
            entity.WellName,
            entity.EventName,
            entity.LossDate,
            entity.DaysOffline
        );
    }
}