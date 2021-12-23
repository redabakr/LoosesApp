using Customer.Application.Exceptions;
using Customer.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Customer.Application.Commands;

public class RemoveCustomer
{
    public class Command : IRequest
    {
        public Guid CustomerId { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull();
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
           
            await _customerRepository.DeleteAsync(customer);
            return Unit.Value;
        }
    }
}