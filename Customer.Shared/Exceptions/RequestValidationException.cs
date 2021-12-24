using Customer.Shared.Abstraction.Exception;

namespace Customer.Shared.Exceptions;

public class RequestValidationException : CustomerServiceBaseException
{
    public Dictionary<string, string[]> ErrorsDictionary { get; }
    public string RequestType { get; }

    public RequestValidationException(string requestType, Dictionary<string, string[]> errorsDictionary): base("Request Validation Error")
    {
        RequestType = requestType;
        ErrorsDictionary = errorsDictionary;
    }
}