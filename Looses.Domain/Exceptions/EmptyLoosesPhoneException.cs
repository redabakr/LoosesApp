using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class EmptyLoosesPhoneException : LoosesServiceBaseException
{
    public EmptyLoosesPhoneException() : base("Customer Phone Cannot be Empty!")
    {
    }
}