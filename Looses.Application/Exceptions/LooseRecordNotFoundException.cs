using Looses.Shared.Abstraction.Exception;

namespace Looses.Application.Exceptions;

public class LooseRecordNotFoundException : LoosesServiceBaseException
{
    public int Id;
    public LooseRecordNotFoundException(int id) : base($"Loose record '{id}' was not found!")
    {
        Id = id;
    }
}