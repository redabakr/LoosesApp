using Customer.Domain.Exceptions;

namespace Customer.Domain.ValueObjects;

public record CustomerFirstName
{
    public string Value { get; }

    public CustomerFirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyCustomerFirstNameException();
        }
        Value = value;
    }

    public static implicit operator string(CustomerFirstName customerFirstName) => customerFirstName.Value;
    public static implicit operator CustomerFirstName(string value) => new(value);
}