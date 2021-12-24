using Customer.Application.Exceptions;
using Customer.Application.Services;
using Customer.Domain.Consts;
using Customer.Domain.Factories;
using Customer.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Customer.Application.Commands;

public sealed class UpdateCustomer
{
    public sealed record Command(Guid Id, string FirstName, string LastName, uint Age, string Phone,
        string Country, string City, Gender Gender) : IRequest;
    
    public sealed class CommandValidator : AbstractValidator<Command>
    {
        private const uint MaxAge = 12;
        public CommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Age).GreaterThan(MaxAge);
            RuleFor(x => x.Phone).NotEmpty();
        }
    }//CommandValidator
    private sealed class CommandHandler : IRequestHandler<Command>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerReadService _customerReadService;
        private readonly ICustomerFactory _customerFactory;

        public CommandHandler(ICustomerRepository customerRepository,
            ICustomerFactory customerFactory,
            ICustomerReadService customerReadService)
        {
            _customerRepository = customerRepository;
            _customerFactory = customerFactory;
            _customerReadService = customerReadService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetAsync(request.Id);
            if (customer is null) throw new CustomerNotFoundException(request.Id);

            //TODO update customer logic 
            
            await _customerRepository.UpdateAsync(customer);
        
            return Unit.Value;
        }
    }//CommandHandler
}

