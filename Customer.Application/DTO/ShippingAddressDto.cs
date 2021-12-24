namespace Customer.Application.DTO;

public record ShippingAddressDto(string Name, string City, string Street, string Description, bool IsDefault);