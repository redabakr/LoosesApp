using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class EmptyLoosesFirstNameException : LoosesServiceBaseException
{
    public EmptyLoosesFirstNameException() : base("Customer First Name Cannot be Empty!")
    {
    }
}