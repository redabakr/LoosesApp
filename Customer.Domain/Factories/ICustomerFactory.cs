using Customer.Domain.Consts;
using Customer.Domain.ValueObjects;

namespace Customer.Domain.Factories;

public interface ICustomerFactory
{
    Entities.Customer Create(CustomerId id,CustomerEmail email, CustomerFirstName firstName, CustomerLastName lastName, CustomerAge age,
        CustomerPhone phone, Gender gender, CustomerCountry country, CustomerCity city);
    
    Entities.Customer CreateWithDefaultAddress(CustomerId id,CustomerEmail email, CustomerFirstName firstName, CustomerLastName lastName,
        CustomerAge age, CustomerPhone phone, Gender gender, CustomerCountry country, CustomerCity city, 
        ShippingAddress defaultAddresses);
}