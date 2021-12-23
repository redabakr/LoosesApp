using Customer.Application.Exceptions;
using Customer.Domain.Repositories;
using Customer.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Customer.Application.Commands;

public class AddCustomerShippingAddress
{
    public class Command : IRequest
    {
        public Guid CustomerId { get; set; }
        public string Name { get; }
        public string City { get; }
        public string Street { get; }
        public string Description { get;  }
        public bool IsDefault { get; init; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
        }
    }
    protected class CommandHandler: IRequestHandler<Command>
    {
        private readonly ICustomerRepository _customerRepository;

        public CommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(request.CustomerId);
            if (customer is null) throw new CustomerNotFoundException(request.CustomerId);

            var shippingAddress = new ShippingAddress(request.Name, request.City, request.Street, request.Description);
            customer.AddShippingAddress(shippingAddress);
            if (shippingAddress.IsDefault)
            {
                customer.SetDefaultShippingAddress(shippingAddress.Name);
            }
            await _customerRepository.UpdateAsync(customer);
            return Unit.Value;
        }
    }
}