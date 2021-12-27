using Looses.Domain.ValueObjects;
using Looses.Shared.Abstraction.Domain;

namespace Looses.Domain.Events;

public record ShippingAddressRemoved(Entities.Looses Looses, ShippingAddress ShippingAddress): IDomainEvent;