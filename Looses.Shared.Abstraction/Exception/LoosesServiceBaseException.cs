namespace Looses.Shared.Abstraction.Exception;

public abstract class LoosesServiceBaseException : System.Exception
{
    protected LoosesServiceBaseException(string message): base(message){}
}