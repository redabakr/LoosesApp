using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class  EmptyShippingAddressNameException: CustomerServiceBaseException
{
    public EmptyShippingAddressNameException() : base("Shipping Address Name Cannot be Empty!")
    {
    }
}