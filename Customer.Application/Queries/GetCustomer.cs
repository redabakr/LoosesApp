using Customer.Application.DTO;
using Customer.Application.Services;
using MediatR;

namespace Customer.Application.Queries;

public sealed class GetCustomer
{
    public sealed record Query(Guid Id) : IRequest<CustomerDto>;
    private sealed class QueryHandler: IRequestHandler<Query,CustomerDto>
    {
        private readonly ICustomerReadService _customerReadService;

        public QueryHandler(ICustomerReadService customerReadService)
        {
            _customerReadService = customerReadService;
        }

        public async Task<CustomerDto?> Handle(Query query, CancellationToken cancellationToken)
        {
            return await _customerReadService.GetCustomerDetails(query.Id);
        }
    }
}