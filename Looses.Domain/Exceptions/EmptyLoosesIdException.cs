using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class EmptyLoosesIdException: LoosesServiceBaseException
{
    public EmptyLoosesIdException() : base("Customer Id Cannot be Empty!")
    { }
}