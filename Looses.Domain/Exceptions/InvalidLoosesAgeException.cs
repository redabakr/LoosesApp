using Looses.Shared.Abstraction.Exception;

namespace Looses.Domain.Exceptions;

public class InvalidLoosesAgeException : LoosesServiceBaseException
{
    public uint Age { get; }
    public InvalidLoosesAgeException(uint age) : base($"Invalid Customer Age '{age}'!")
        => Age = age;
}