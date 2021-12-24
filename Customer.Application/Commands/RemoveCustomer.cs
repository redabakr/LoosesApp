using Customer.Application.Exceptions;
using Customer.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Customer.Application.Commands;

public sealed class RemoveCustomer
{
    public sealed record Command(Guid Id) : IRequest;

    public sealed class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
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
            var customer = await _customerRepository.GetAsync(request.Id);
            if (customer is null) throw new CustomerNotFoundException(request.Id);
           
            await _customerRepository.DeleteAsync(customer);
            return Unit.Value;
        }
    }
}