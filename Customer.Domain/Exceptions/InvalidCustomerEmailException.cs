using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class InvalidCustomerEmailException : CustomerServiceBaseException
{
    public string Email;

    public InvalidCustomerEmailException(string email) : base($"Invalid Customer Email '{email}'")
    {
        Email = email;
    }
}