using Looses.Shared.Abstraction.Exception;

namespace Looses.Application.Exceptions;

public class LossAlreadyReportedException: LoosesServiceBaseException
{
    public string WellName { get; }
    public string EventName { get; }
    public LossAlreadyReportedException(string wellName, string eventName) : base($"Loss entry already exits for '{wellName}' for event '{eventName}'!")
    {
        WellName = wellName;
        EventName = eventName;
    }
}