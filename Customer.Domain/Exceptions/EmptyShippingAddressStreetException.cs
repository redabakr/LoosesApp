using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class  EmptyShippingAddressStreetException: CustomerServiceBaseException
{
    public EmptyShippingAddressStreetException() : base("Shipping Address Street Cannot be Empty!")
    {
    }
}