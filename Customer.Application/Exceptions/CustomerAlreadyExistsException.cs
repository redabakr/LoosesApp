using Customer.Shared.Abstraction.Exception;

namespace Customer.Application.Exceptions;

public class CustomerAlreadyExistsException: CustomerServiceBaseException
{
    public string Email { get; }
    public CustomerAlreadyExistsException(string email) : base($"Customer '{email}' already exists!")
    {
        Email = email;
    }
}