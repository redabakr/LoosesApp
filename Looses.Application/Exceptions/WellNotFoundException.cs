using Looses.Shared.Abstraction.Exception;

namespace Looses.Application.Exceptions;

public class WellNotFoundException: LoosesServiceBaseException
{
    public string WellName { get; }
    public WellNotFoundException(string wellName) : base($"Well '{wellName}' was not found!")
    {
        WellName = wellName;
    }
}