using Customer.Domain.Exceptions;

namespace Customer.Domain.ValueObjects;

public record CustomerCity
{
    public string Value { get; }

    public CustomerCity(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCustomerCityException();
        }
        Value = value;
    }

    public static implicit operator string(CustomerCity customerCity) => customerCity.Value;
    public static implicit operator CustomerCity(string value) => new(value);
}