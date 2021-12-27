using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class  EmptyShippingAddressCityException: LoosesServiceBaseException
{
    public EmptyShippingAddressCityException() : base("Shipping Address City Cannot be Empty!")
    {
    }
}