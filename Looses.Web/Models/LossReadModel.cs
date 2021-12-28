namespace Looses.Web.Models;
public record LossReadModel(int Id, string WellName, string EventName, DateTime LoosDate, int DaysOffline);