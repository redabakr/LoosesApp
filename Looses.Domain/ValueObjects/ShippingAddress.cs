using Looses.Domain.Exceptions;

namespace Looses.Domain.ValueObjects;

public record ShippingAddress
{
    public ShippingAddress(string name, string city, string street, string description, bool isDefault = false)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new EmptyShippingAddressNameException();
        }
        if (string.IsNullOrWhiteSpace(city))
        {
            throw new EmptyShippingAddressCityException();
        }
        if (string.IsNullOrWhiteSpace(street))
        {
            throw new EmptyShippingAddressStreetException();
        }
        
        Name = name;
        City = city;
        Street = street;
        Description = description;
        IsDefault = isDefault;
    }

    private Guid Id;
    public string Name { get; }
    public string City { get; }
    public string Street { get; }
    public string Description { get;  }
    public bool IsDefault { get; init; }
}