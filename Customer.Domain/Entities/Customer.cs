using Customer.Domain.Consts;
using Customer.Domain.Events;
using Customer.Domain.Exceptions;
using Customer.Domain.ValueObjects;
using Customer.Shared.Abstraction.Domain;

namespace Customer.Domain.Entities;

public class Customer : AggregateRoot<CustomerId>
{
   // public CustomerId Id { get; private set; }
    public CustomerEmail Email { get; private set; }
    public CustomerFirstName FirstName { get; private set; }
    public CustomerLastName LastName { get; private set; }
    public CustomerAge Age { get; private set; }
    public CustomerPhone Phone { get; private set; }
    public Gender Gender { get; private set; }
    public CustomerCountry Country { get; private set; }
    public CustomerCity City { get;  private set; }

    private readonly LinkedList<ShippingAddress> _shippingAddresses = new();

    private Customer()
    {
        
    }
    private Customer(CustomerId id, CustomerEmail email, CustomerFirstName firstName, CustomerLastName lastName, CustomerAge age,
        CustomerPhone phone, Gender gender, CustomerCountry country, CustomerCity city, ShippingAddress defaultAddresses) 
        : this(id, email ,firstName, lastName, age, phone, gender, country, city)
    {
        _shippingAddresses.AddFirst(defaultAddresses);
    }
    internal Customer(CustomerId id,CustomerEmail email, CustomerFirstName firstName, CustomerLastName lastName, CustomerAge age,
        CustomerPhone phone, Gender gender, CustomerCountry country, CustomerCity city)  
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Phone = phone;
        Gender = gender;
        Country = country;
        City = city;
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

    public void RemoveShippingAddress(string shippingAddressName)
    {
        var address = GetShippingAddress(shippingAddressName);
        _shippingAddresses.Remove(address);
        AddEvent(new ShippingAddressRemoved(this, address));
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


