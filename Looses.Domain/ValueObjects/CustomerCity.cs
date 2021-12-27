using Looses.Domain.Exceptions;

namespace Looses.Domain.ValueObjects;

public record CustomerCity
{
    public string Value { get; }

    public CustomerCity(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyLoosesCityException();
        }
        Value = value;
    }

    public static implicit operator string(CustomerCity customerCity) => customerCity.Value;
    public static implicit operator CustomerCity(string value) => new(value);
}