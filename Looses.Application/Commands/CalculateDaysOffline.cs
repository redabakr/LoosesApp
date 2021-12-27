using Looses.Domain.Repositories;
using MediatR;

namespace Looses.Application.Commands;

public sealed class CalculateDaysOffline
{
    public sealed record Command() : IRequest;
    
    private sealed class CommandHandler : IRequestHandler<Command>
    {
        private readonly ILooseRepository _looseRepository;

        public CommandHandler(ILooseRepository looseRepository)
        {
            _looseRepository = looseRepository;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var records = await _looseRepository.GetAllAsync();
            
            if(!records.Any()) return Unit.Value;

            foreach (var entry in records)
            {
                var prevDay = entry.LossDate.AddDays(-1);
                var offlineDays = 1;

                var prevEntry = records.FirstOrDefault(x =>
                    x.WellName == entry.WellName && x.EventName == entry.EventName && x.LossDate == prevDay);
                
                if (prevEntry is not null)
                {
                    offlineDays = prevEntry.DaysOffline + 1;
                }
                entry.UpdateOfflineDays(offlineDays);
                await _looseRepository.UpdateAsync(entry);
            }


            await _looseRepository.SaveChangesAsync();
            return Unit.Value;
        }

        
    }//CommandHandler
}

