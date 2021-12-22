using Customer.Domain.Exceptions;

namespace Customer.Domain.ValueObjects;

public record CustomerLastName
{
    public string Value { get; }

    public CustomerLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCustomerLastNameException();
        }
        Value = value;
    }

    public static implicit operator string(CustomerLastName customerLastName) => customerLastName.Value;
    public static implicit operator CustomerLastName(string value) => new(value);
}