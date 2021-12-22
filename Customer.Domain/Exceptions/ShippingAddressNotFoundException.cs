using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class ShippingAddressNotFoundException : CustomerServiceBaseException
{
    public string Name { get; }
    public ShippingAddressNotFoundException(string name) : base($"Shipping Address '{name}' was not Found!")
        => Name = name;
}