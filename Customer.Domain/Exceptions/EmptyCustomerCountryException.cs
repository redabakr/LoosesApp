using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class EmptyCustomerCountryException : CustomerServiceBaseException
{
    public EmptyCustomerCountryException() : base("Customer Country Cannot be Empty!")
    {
    }
}