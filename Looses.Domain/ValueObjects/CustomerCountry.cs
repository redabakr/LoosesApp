using Looses.Domain.Exceptions;

namespace Looses.Domain.ValueObjects;

public record CustomerCountry
{
    public string Value { get; }

    public CustomerCountry(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyLoosesCountryException();
        }
        Value = value;
    }

    public static implicit operator string(CustomerCountry customerCountry) => customerCountry.Value;
    public static implicit operator CustomerCountry(string value) => new(value);
}