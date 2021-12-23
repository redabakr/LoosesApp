using Customer.Domain.ValueObjects;

namespace Customer.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Entities.Customer?> GetAsync(CustomerId id);
    Task AddAsync(Entities.Customer customer);
    Task UpdateAsync(Entities.Customer customer);
    Task DeleteAsync(Entities.Customer customer);
}