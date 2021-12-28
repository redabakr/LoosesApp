using FluentValidation;
using Looses.Application.DTO;
using Looses.Application.Exceptions;
using Looses.Application.Services;
using Looses.Domain.Repositories;
using MediatR;

namespace Looses.Application.Commands;

public sealed class CreateLooseRecord
{
    public sealed record Command(string WellName, string EventName, DateTime LossDate) : IRequest<LossReadDto>;
    
    public sealed class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.WellName).NotEmpty().MaximumLength(50).WithMessage("Well name must not exceed 50 characters.");;
            RuleFor(x => x.EventName).NotEmpty();
            RuleFor(x => x.LossDate).Must(IsValidDate);
        }
        private static bool IsValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }//CommandValidator
    private sealed class CommandHandler : IRequestHandler<Command,LossReadDto>
    {
        private readonly ILooseRepository _looseRepository;
        private readonly ILoosesReadService _loosesReadService;
        public CommandHandler(ILooseRepository looseRepository,
            ILoosesReadService loosesReadService)
        {
            _looseRepository = looseRepository;
            _loosesReadService = loosesReadService;
        }

        public async Task<LossReadDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var isWellExists = await _loosesReadService.ExistsWellByNameAsync(request.WellName);
            // check if well exists
            if (!isWellExists)
            {
                throw new WellNotFoundException(request.WellName);
            }
            
            var isLossRecordExists = await _loosesReadService.LossRecordForSameDayExistsAsync(request.WellName, request.EventName, request.LossDate);
            // check if well exists
            if (isLossRecordExists)
            {
                throw new LossAlreadyReportedException(request.WellName, request.EventName);
            }
            
            var looseRecord = new Domain.Entities.Looses(request.WellName, request.EventName, request.LossDate);

            // var daysOffline = 1;
            // var previousLossDate = request.LossDate.AddDays(-1);
            //
            // var wellLooses = await _loosesReadService.GetLoosesForWell(request.WellName, previousLossDate);
            // wellLooses =  wellLooses.OrderByDescending(x => x.LoosDate);
            // if (wellLooses.Any())
            // {
            //     daysOffline = wellLooses.LastOrDefault().DaysOffline + 1;
            // }
            //
            // looseRecord.UpdateOfflineDays(daysOffline);
            
            await _looseRepository.AddAsync(looseRecord);
            await _looseRepository.SaveChangesAsync();

            return looseRecord.AsDto();
        }
    }//CommandHandler
    
}

