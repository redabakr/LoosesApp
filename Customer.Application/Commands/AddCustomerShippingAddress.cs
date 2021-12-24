using Customer.Application.Exceptions;
using Customer.Domain.Repositories;
using Customer.Domain.ValueObjects;
using FluentValidation;
using MediatR;

namespace Customer.Application.Commands;

public sealed class AddCustomerShippingAddress
{
    public sealed record Command
        (Guid CustomerId, string Name, string City, string Street, string Description, bool IsDefault) : IRequest;

    public sealed class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
        }
    }
    private sealed class CommandHandler: IRequestHandler<Command>
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

            var shippingAddress = new ShippingAddress(request.Name, request.City, request.Street, request.Description, request.IsDefault);
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