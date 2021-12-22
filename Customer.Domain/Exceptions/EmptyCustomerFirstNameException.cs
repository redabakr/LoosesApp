using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class EmptyCustomerFirstNameException : CustomerServiceBaseException
{
    public EmptyCustomerFirstNameException() : base("Customer First Name Cannot be Empty!")
    {
    }
}