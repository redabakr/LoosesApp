using Looses.Domain.ValueObjects;
using Looses.Shared.Abstraction.Domain;

namespace Looses.Domain.Events;

public record DefaultShippingAddressUpdated(Entities.Looses Looses, ShippingAddress ShippingAddress): IDomainEvent;