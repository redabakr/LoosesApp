using Customer.Domain.ValueObjects;
using Customer.Shared.Abstraction.Domain;

namespace Customer.Domain.Events;

public record ShippingAddressAdded(Entities.Customer Customer, ShippingAddress ShippingAddress) : IDomainEvent;