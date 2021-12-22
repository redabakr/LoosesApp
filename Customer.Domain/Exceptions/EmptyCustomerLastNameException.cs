using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class EmptyCustomerLastNameException : CustomerServiceBaseException
{
    public EmptyCustomerLastNameException() : base("Customer Last Name Cannot be Empty!")
    {
    }
}