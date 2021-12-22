using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class EmptyCustomerCityException : CustomerServiceBaseException
{
    public EmptyCustomerCityException() : base("Customer City Cannot be Empty!")
    {
    }
}

