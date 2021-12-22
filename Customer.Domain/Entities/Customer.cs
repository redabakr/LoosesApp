using Customer.Domain.Consts;
using Customer.Domain.Events;
using Customer.Domain.Exceptions;
using Customer.Domain.ValueObjects;
using Customer.Shared.Abstraction.Domain;

namespace Customer.Domain.Entities;

public class Customer : AggregateRoot<CustomerId>
{
    public CustomerId Id { get; private set; }
    private CustomerFirstName _firstName;
    private CustomerLastName _lastName;
    private CustomerAge _age;
    private CustomerPhone _phone;
    private Gender _gender;
    private CustomerCountry _country;
    private CustomerCity _city;

    private readonly LinkedList<ShippingAddress> _shippingAddresses = new();

    private Customer()
    {
    }
    private Customer(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerAge age,
        CustomerPhone phone, Gender gender, CustomerCountry country, CustomerCity city, LinkedList<ShippingAddress> addresses) 
        : this(id, firstName, lastName, age, phone, gender, country, city)
    {
        _shippingAddresses = addresses;
    }
    internal Customer(CustomerId id, CustomerFirstName firstName, CustomerLastName lastName, CustomerAge age,
        CustomerPhone phone, Gender gender, CustomerCountry country, CustomerCity city)  
    {
        Id = id;
        _firstName = firstName;
        _lastName = lastName;
        _age = age;
        _phone = phone;
        _gender = gender;
        _country = country;
        _city = city;
    }

    public void AddShippingAddress(ShippingAddress shippingAddress)
    {
        var alreadyExist = _shippingAddresses.Any(x => x.Name == shippingAddress.Name);
        if (alreadyExist)
        {
            throw new ShippingAddressAlreadyExistException(shippingAddress.Name);
        }
        _shippingAddresses.AddLast(shippingAddress);
        AddEvent(new ShippingAddressAdded(this, shippingAddress));
    }

    public void SetDefaultShippingAddress(string shippingAddressName)
    {
        var address = GetShippingAddress(shippingAddressName);
        var defaultAddress = _shippingAddresses.FirstOrDefault(x => x.IsDefault);
        if (defaultAddress is not null)
        {
            var oldDefaultedAddress = defaultAddress with { IsDefault = true };
            _shippingAddresses.Find(defaultAddress)!.Value = oldDefaultedAddress;
        }
        var newDefaultAddress = address with { IsDefault = true };
        _shippingAddresses.Find(address)!.Value = newDefaultAddress;
        AddEvent(new DefaultShippingAddressUpdated(this, address));
    }

    private ShippingAddress GetShippingAddress(string name)
    {
        var address = _shippingAddresses.FirstOrDefault(x => x.Name == name);
        if (address is null) 
        {
            throw new ShippingAddressNotFoundException(name); 
        }
        return address;
    }
     
}


