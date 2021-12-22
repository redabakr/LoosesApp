using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class EmptyCustomerPhoneException : CustomerServiceBaseException
{
    public EmptyCustomerPhoneException() : base("Customer Phone Cannot be Empty!")
    {
    }
}