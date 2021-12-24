using Customer.Domain.Consts;

namespace Customer.Application.DTO;

public record CustomerDto(Guid Id, string Email, string FirstName, string LastName, uint Age, string Phone,
    string Country, string City, Gender Gender, IEnumerable<ShippingAddressDto>? ShippingAddresses);