using Looses.Application.DTO;
using Looses.Application.Services;
using MediatR;

namespace Looses.Application.Queries;

public sealed class GetLooseDetails
{
    public sealed record Query(int Id) : IRequest<LossReadDto>;
    private sealed class QueryHandler: IRequestHandler<Query,LossReadDto>
    {
        private readonly ILoosesReadService _loosesReadService;

        public QueryHandler(ILoosesReadService loosesReadService)
        {
            _loosesReadService = loosesReadService;
        }

        public async Task<LossReadDto?> Handle(Query query, CancellationToken cancellationToken)
        {
            return await _loosesReadService.GetlooseDetails(query.Id);
        }
    }
}