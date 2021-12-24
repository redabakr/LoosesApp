using Customer.Application.DTO;
using Customer.Application.Services;
using MediatR;

namespace Customer.Application.Queries;

public sealed class GetCustomers
{
    public sealed record Query(string SearchPhrase ) : IRequest<IEnumerable<CustomerDto>>;
    private sealed class QueryHandler: IRequestHandler<Query,IEnumerable<CustomerDto>>
    {
        private readonly ICustomerReadService _customerReadService;

        public QueryHandler(ICustomerReadService customerReadService)
        {
            _customerReadService = customerReadService;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            return await _customerReadService.GetCustomers(query.SearchPhrase);
        }
    }
}