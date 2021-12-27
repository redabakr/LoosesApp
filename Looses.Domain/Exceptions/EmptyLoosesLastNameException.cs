using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class EmptyLoosesLastNameException : LoosesServiceBaseException
{
    public EmptyLoosesLastNameException() : base("Customer Last Name Cannot be Empty!")
    {
    }
}