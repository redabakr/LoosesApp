using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class InvalidLoosesEmailException : LoosesServiceBaseException
{
    public string Email;

    public InvalidLoosesEmailException(string email) : base($"Invalid Customer Email '{email}'")
    {
        Email = email;
    }
}