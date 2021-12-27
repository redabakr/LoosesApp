using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class ShippingAddressNotFoundException : LoosesServiceBaseException
{
    public string Name { get; }
    public ShippingAddressNotFoundException(string name) : base($"Shipping Address '{name}' was not Found!")
        => Name = name;
}