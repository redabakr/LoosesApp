using Customer.Shared.Abstraction.Exception;

namespace Customer.Domain.Exceptions;

public class InvalidCustomerAgeException : CustomerServiceBaseException
{
    public uint Age { get; }
    public InvalidCustomerAgeException(uint age) : base($"Invalid Customer Age '{age}'!")
        => Age = age;
}