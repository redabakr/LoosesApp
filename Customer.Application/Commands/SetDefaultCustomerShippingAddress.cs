using Customer.Application.Exceptions;
using Customer.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Customer.Application.Commands;

public sealed class SetDefaultCustomerShippingAddress
{
    public sealed record Command(Guid CustomerId, string ShippingAddressName) : IRequest;

    public sealed class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull();
            RuleFor(x => x.ShippingAddressName).NotEmpty();
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
           
            customer.SetDefaultShippingAddress(request.ShippingAddressName);
            
            await _customerRepository.UpdateAsync(customer);
            return Unit.Value;
        }
    }
}