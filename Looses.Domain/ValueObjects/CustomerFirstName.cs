using Looses.Domain.Exceptions;

namespace Looses.Domain.ValueObjects;

public record CustomerFirstName
{
    public string Value { get; }

    public CustomerFirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyLoosesFirstNameException();
        }
        Value = value;
    }

    public static implicit operator string(CustomerFirstName customerFirstName) => customerFirstName.Value;
    public static implicit operator CustomerFirstName(string value) => new(value);
}