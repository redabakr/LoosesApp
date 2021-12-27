using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class  EmptyShippingAddressStreetException: LoosesServiceBaseException
{
    public EmptyShippingAddressStreetException() : base("Shipping Address Street Cannot be Empty!")
    {
    }
}