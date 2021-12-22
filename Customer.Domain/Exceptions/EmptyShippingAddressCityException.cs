using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class  EmptyShippingAddressCityException: CustomerServiceBaseException
{
    public EmptyShippingAddressCityException() : base("Shipping Address City Cannot be Empty!")
    {
    }
}