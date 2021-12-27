using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class ShippingAddressAlreadyExistException : LoosesServiceBaseException
{
    public string Name { get; }
    public ShippingAddressAlreadyExistException(string name) : base($"Shipping Address '{name}' Already Exists!")
        => Name = name;
}