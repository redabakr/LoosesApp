using Looses.Shared.Abstraction.Exception;

namespace Looses.Shared.Exceptions;

public class RequestValidationException : LoosesServiceBaseException
{
    public Dictionary<string, string[]> ErrorsDictionary { get; }
    public string RequestType { get; }

    public RequestValidationException(string requestType, Dictionary<string, string[]> errorsDictionary): base("Request Validation Error")
    {
        RequestType = requestType;
        ErrorsDictionary = errorsDictionary;
    }
}