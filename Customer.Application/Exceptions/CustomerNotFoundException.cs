using Customer.Shared.Abstraction.Exception;

namespace Customer.Application.Exceptions;

public class CustomerNotFoundException : CustomerServiceBaseException
{
    public Guid CustomerId;
    public CustomerNotFoundException(Guid customerId) : base($"Customer '{customerId}' was not found!")
    {
        CustomerId = customerId;
    }
}