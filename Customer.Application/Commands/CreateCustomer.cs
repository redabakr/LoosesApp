using Customer.Application.Exceptions;
using Customer.Application.Services;
using Customer.Domain.Consts;
using Customer.Domain.Factories;
using Customer.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Customer.Application.Commands;

public class CreateCustomer
{
    public sealed record Command(Guid Id, string Email, string FirstName, string LastName, uint Age, string Phone,
        string Country, string City, Gender Gender) : IRequest;
    
    public sealed class CommandValidator : AbstractValidator<Command>
    {
        private const uint MaxAge = 12;
        public CommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Age).GreaterThan(MaxAge);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone).NotEmpty();
        }
    }//CommandValidator
    protected sealed class CommandHandler : IRequestHandler<Command>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerFactory _customerFactory;
        private readonly ICustomerReadService _customerReadService;
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
            // check if customer with the same email exists
            if (await _customerReadService.ExistsByEmailAsync(request.Email))
            {
                throw new CustomerAlreadyExistsException(request.Email);
            }
            
            var customer = _customerFactory.Create(request.Id, request.Email, request.FirstName, request.LastName,
                request.Age, request.Phone, request.Gender, request.Country, request.City);
        
            await _customerRepository.AddAsync(customer);
        
            return Unit.Value;
        }
    }//CommandHandler

    
}

