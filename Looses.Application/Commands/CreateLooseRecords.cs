using FluentValidation;
using Looses.Application.DTO;
using Looses.Application.Exceptions;
using Looses.Application.Services;
using Looses.Domain.Repositories;
using MediatR;

namespace Looses.Application.Commands;

public sealed class CreateLooseRecords
{
    public sealed record Command(List<LossWriteDto> LoosesRecords) : IRequest;
    
    public sealed class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleForEach(x => x.LoosesRecords).SetValidator(new LoosesDataValidator());
        }
    }//CommandValidator

    private sealed class LoosesDataValidator : AbstractValidator<LossWriteDto>
    {
        public LoosesDataValidator()
        {
            RuleFor(x => x.WellName).NotEmpty().MaximumLength(50).WithMessage("Well name must not exceed 50 characters.");;
            RuleFor(x => x.EventName).NotNull().MaximumLength(100);
            RuleFor(x => x.LossDate).NotNull().Must(IsValidDate);
        }
        private static bool IsValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }//CommandValidator
    private sealed class CommandHandler : IRequestHandler<Command>
    {
        private readonly ILooseRepository _looseRepository;
        private readonly ILoosesReadService _loosesReadService;
        public CommandHandler(ILooseRepository looseRepository,
            ILoosesReadService loosesReadService)
        {
            _looseRepository = looseRepository;
            _loosesReadService = loosesReadService;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            foreach (var record in request.LoosesRecords)
            {
                var isWellExists = await _loosesReadService.ExistsWellByNameAsync(record.WellName);
                // check if well exists
                if (!isWellExists)
                {
                    throw new WellNotFoundException(record.WellName);
                }

                var looseRecord = new Domain.Entities.Looses(record.WellName, record.EventName, record.LossDate);
                // var daysOffline = 0;
                // var previousLossDate = record.LossDate.AddDays(-1);
                // looseRecord.UpdateOfflineDays(daysOffline);
                await _looseRepository.AddAsync(looseRecord);
            }
            await _looseRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }//CommandHandler
    
}

