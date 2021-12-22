using Customer.Domain.ValueObjects;
using Customer.Shared.Abstraction.Domain;

namespace Customer.Domain.Events;

public record DefaultShippingAddressUpdated(Entities.Customer Customer, ShippingAddress ShippingAddress): IDomainEvent;