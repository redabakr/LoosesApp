using Customer.Domain.Repositories;
using Customer.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.EF.Repositories;

internal sealed class EfCustomerRepository: ICustomerRepository
{
    private readonly DbSet<Domain.Entities.Customer> _customers;
    private readonly CustomerDbContext _context;

    public EfCustomerRepository(CustomerDbContext context)
    {
        _customers = context.Customers;
        _context = context;
    }

    public async Task<Domain.Entities.Customer?> GetAsync(CustomerId id) =>
       await _customers.Include(x=> x.ShippingAddresses).FirstOrDefaultAsync(x => x.Id == id);
    public async Task AddAsync(Domain.Entities.Customer customer)
    {
        await _customers.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.Customer customer)
    {
         _customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Domain.Entities.Customer customer)
    {
         _customers.Remove(customer);
        await _context.SaveChangesAsync();
    }
}