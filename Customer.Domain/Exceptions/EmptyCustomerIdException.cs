using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class EmptyCustomerIdException: CustomerServiceBaseException
{
    public EmptyCustomerIdException() : base("Customer Id Cannot be Empty!")
    { }
}