using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class EmptyLoosesCountryException : LoosesServiceBaseException
{
    public EmptyLoosesCountryException() : base("Customer Country Cannot be Empty!")
    {
    }
}