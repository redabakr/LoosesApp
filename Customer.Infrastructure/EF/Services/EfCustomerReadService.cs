using Customer.Application.DTO;
using Customer.Application.Services;
using Customer.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.EF.Services;

internal sealed class EfCustomerReadService : ICustomerReadService
{
    private readonly DbSet<Domain.Entities.Customer> _customers;

    public EfCustomerReadService(CustomerDbContext context)
    {
        _customers = context.Customers;
    }

    public async Task<CustomerDto> GetCustomerDetails(Guid id)
    {
    var customer = await _customers
        .Include(x=> x.ShippingAddresses)
        .SingleOrDefaultAsync(x => x.Id == new CustomerId(id));
    if (customer is null) return new CustomerDto();
    return new CustomerDto()
    {
        Id = customer.Id,
        FirstName = customer.FirstName,
        LastName = customer.LastName,
        Email = customer.Email,
        Country = customer.Country,
        City = customer.City,
        Age = customer.Age.Value
    };
    }

    public async Task<bool> ExistsByEmailAsync(string email) => await _customers.AnyAsync(x => x.Email == email);
}