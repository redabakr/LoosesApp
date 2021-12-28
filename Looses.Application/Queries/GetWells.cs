﻿using Looses.Application.DTO;
using Looses.Application.Services;
using MediatR;

namespace Looses.Application.Queries;

public sealed class GetWells
{
    public sealed record Query() : IRequest<IEnumerable<WellDto>>;
    private sealed class QueryHandler: IRequestHandler<Query,IEnumerable<WellDto>>
    {
        private readonly ILoosesReadService _loosesReadService;

        public QueryHandler(ILoosesReadService loosesReadService)
        {
            _loosesReadService = loosesReadService;
        }

        public async Task<IEnumerable<WellDto>> Handle(Query query, CancellationToken cancellationToken)
        {
            return await _loosesReadService.GetWells();
        }
    }
}