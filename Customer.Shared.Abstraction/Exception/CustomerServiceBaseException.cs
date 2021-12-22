namespace Customer.Shared.Abstraction.Exception;

public abstract class CustomerServiceBaseException : System.Exception
{
    protected CustomerServiceBaseException(string message): base(message){}
}