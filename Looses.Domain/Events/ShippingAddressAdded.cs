using Looses.Domain.ValueObjects;
using Looses.Shared.Abstraction.Domain;

namespace Looses.Domain.Events;

public record ShippingAddressAdded(Entities.Looses Looses, ShippingAddress ShippingAddress) : IDomainEvent;