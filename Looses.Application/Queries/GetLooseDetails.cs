using Looses.Application.DTO;
using Looses.Application.Services;
using MediatR;

namespace Looses.Application.Queries;

public sealed class GetLooseDetails
{
    public sealed record Query(int Id) : IRequest<LoosesDto>;
    private sealed class QueryHandler: IRequestHandler<Query,LoosesDto>
    {
        private readonly ILoosesReadService _loosesReadService;

        public QueryHandler(ILoosesReadService loosesReadService)
        {
            _loosesReadService = loosesReadService;
        }

        public async Task<LoosesDto?> Handle(Query query, CancellationToken cancellationToken)
        {
            return await _loosesReadService.GetlooseDetails(query.Id);
        }
    }
}