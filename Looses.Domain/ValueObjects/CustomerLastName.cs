using Looses.Domain.Exceptions;

namespace Looses.Domain.ValueObjects;

public record CustomerLastName
{
    public string Value { get; }

    public CustomerLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyLoosesLastNameException();
        }
        Value = value;
    }

    public static implicit operator string(CustomerLastName customerLastName) => customerLastName.Value;
    public static implicit operator CustomerLastName(string value) => new(value);
}