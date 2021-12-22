using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class ShippingAddressAlreadyExistException : CustomerServiceBaseException
{
    public string Name { get; }
    public ShippingAddressAlreadyExistException(string name) : base($"Shipping Address '{name}' Already Exists!")
        => Name = name;
}