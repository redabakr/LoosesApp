﻿using Looses.Application.DTO;
using Looses.Application.Services;
using MediatR;

namespace Looses.Application.Queries;

public sealed class GetLooses
{
    public sealed record Query(string? WellName ) : IRequest<IEnumerable<LossReadDto>>;
    private sealed class QueryHandler: IRequestHandler<Query,IEnumerable<LossReadDto>>
    {
        private readonly ILoosesReadService _loosesReadService;

        public QueryHandler(ILoosesReadService loosesReadService)
        {
            _loosesReadService = loosesReadService;
        }

        public async Task<IEnumerable<LossReadDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            return await _loosesReadService.GetLooses(query.WellName);
        }
    }
}