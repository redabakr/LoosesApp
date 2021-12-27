using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class EmptyLoosesCityException : LoosesServiceBaseException
{
    public EmptyLoosesCityException() : base("Customer City Cannot be Empty!")
    {
    }
}

