using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class  EmptyShippingAddressNameException: LoosesServiceBaseException
{
    public EmptyShippingAddressNameException() : base("Shipping Address Name Cannot be Empty!")
    {
    }
}