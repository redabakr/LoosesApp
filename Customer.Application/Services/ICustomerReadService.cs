using Customer.Application.DTO;

namespace Customer.Application.Services;

public interface ICustomerReadService
{
    Task<CustomerDto?> GetCustomerDetails(Guid Id);
    Task<bool> ExistsByEmailAsync(string email);

    Task<IEnumerable<CustomerDto>> GetCustomers(string searchPhrase );
}