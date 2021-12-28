namespace Looses.Application.DTO;

public record LossReadDto(int Id, string WellName, string EventName, DateTime LoosDate, int DaysOffline);