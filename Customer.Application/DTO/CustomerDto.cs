using Customer.Domain.Consts;

namespace Customer.Application.DTO;

public class CustomerDto
{
    public Guid Id { get;  set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public uint Age { get; set; }
    public string Phone { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public Gender Gender { get; set; }
}