using Looses.Domain.Exceptions;

namespace Looses.Domain.ValueObjects;

public record CustomerPhone
{
    public string Value { get; }

    public CustomerPhone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyLoosesPhoneException();
        }
        Value = value;
    }

    public static implicit operator string(CustomerPhone customerPhone) => customerPhone.Value;
    public static implicit operator CustomerPhone(string value) => new(value);
}