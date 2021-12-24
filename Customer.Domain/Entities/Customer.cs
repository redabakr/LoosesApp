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
    public IReadOnlyCollection<ShippingAddress> ShippingAddresses => _shippingAddresses;

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
        var address1 = GetShippingAddress(shippingAddressName);
        var address2 = _shippingAddresses.FirstOrDefault(x => x.IsDefault);
        if (address2 is not null && address2.Name != address1.Name)
        {
            var newAddress2 = address2 with { IsDefault = false };
            _shippingAddresses.Find(address2)!.Value = newAddress2;
        }
        var newAddress1 = address1 with { IsDefault = true };
        _shippingAddresses.Find(address1)!.Value = newAddress1;
        AddEvent(new DefaultShippingAddressUpdated(this, newAddress1));
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


