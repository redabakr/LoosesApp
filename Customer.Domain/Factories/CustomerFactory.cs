using Customer.Domain.Consts;
using Customer.Domain.ValueObjects;

namespace Customer.Domain.Factories;

public class CustomerFactory : ICustomerFactory
{
    public Entities.Customer Create(CustomerId id,CustomerEmail email, CustomerFirstName firstName, CustomerLastName lastName,
        CustomerAge age,
        CustomerPhone phone, Gender gender, CustomerCountry country, CustomerCity city)
        => new(id,email, firstName, lastName, age, phone, gender, country, city);

    public Entities.Customer CreateWithDefaultAddress(CustomerId id, CustomerEmail email, CustomerFirstName firstName,
        CustomerLastName lastName, CustomerAge age, CustomerPhone phone, Gender gender, CustomerCountry country,
        CustomerCity city, ShippingAddress defaultAddresses)
    {
        var customer = Create(id, email, firstName, lastName, age, phone, gender, country, city);
        customer.AddShippingAddress(defaultAddresses);
        customer.SetDefaultShippingAddress(defaultAddresses.Name);
        return customer;
    }
}