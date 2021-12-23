using Customer.Application.DTO;
using Customer.Application.Services;

namespace Customer.Infrastructure.EF.Services;

public class EfCustomerReadService : ICustomerReadService
{
    public Task<CustomerDto> GetCustomerDetails(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}