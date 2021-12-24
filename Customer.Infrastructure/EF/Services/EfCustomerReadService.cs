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

    public async Task<CustomerDto?> GetCustomerDetails(Guid id)
    {
        var customer = await _customers
            .Include(x => x.ShippingAddresses)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == new CustomerId(id));
        if (customer is null) return null;
        return AsDto(customer);
    }

    public async Task<bool> ExistsByEmailAsync(string email) => await _customers.AnyAsync(x => x.Email == email);

    public async Task<IEnumerable<CustomerDto>> GetCustomers(string searchPhrase)
    {
        var dbQuery = _customers
            .Include(x => x.ShippingAddresses)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchPhrase))
        {
            dbQuery = dbQuery
                .Where(x => Microsoft.EntityFrameworkCore.EF.Functions.Like(x.FirstName, $"%{searchPhrase}%"));
        }

        return (await dbQuery.AsNoTracking().Select(x => AsDto(x)).ToListAsync());
    }

    private static CustomerDto AsDto(Domain.Entities.Customer customer)
    {
        var shippingAddresses = customer.ShippingAddresses.Select(x =>
            new ShippingAddressDto(x.Name, x.City, x.Street, x.Description, x.IsDefault));
        return new CustomerDto(
                customer.Id,
                customer.Email,
                customer.FirstName,
                customer.LastName,
                customer.Age,
                customer.Phone,
                customer.Country,
                customer.City,
                customer.Gender,
                shippingAddresses
            );
    }
}