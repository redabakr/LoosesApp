using Looses.Domain.Exceptions;

namespace Looses.Domain.ValueObjects;
public record CustomerId
{
    public Guid Value { get; }
    public CustomerId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyLoosesIdException();
        }
        Value = value;
    }
    public static implicit operator Guid(CustomerId customerId) => customerId.Value;
    public static implicit operator CustomerId(Guid value) => new(value);
}