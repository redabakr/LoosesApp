using Customer.Application.DTO;
using Customer.Application.Services;
using MediatR;

namespace Customer.Application.Queries;

public class GetCustomer
{
    public class Query : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }
    }
    protected class QueryHandler: IRequestHandler<Query,CustomerDto>
    {
        private readonly ICustomerReadService _customerReadService;

        public QueryHandler(ICustomerReadService customerReadService)
        {
            _customerReadService = customerReadService;
        }

        public async Task<CustomerDto> Handle(Query query, CancellationToken cancellationToken)
        {
            return await _customerReadService.GetCustomerDetails(query.Id);
        }
    }
}