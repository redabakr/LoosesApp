using Looses.Shared.Abstraction.Domain;

namespace Looses.Domain.Entities;

public class Looses : Entity<int>
{
    public string WellName { get; private set; }
    public string EventName { get; private set; }
    public DateTime LossDate { get; private set; }
    public int DaysOffline { get; private set; }

    public Well Well { get; set; }
    private Looses()
    {
        
    }
    public Looses(string wellName, string eventName, DateTime loosDate)
    {
        this.WellName = wellName;
        this.EventName = eventName;
        this.LossDate = loosDate;
    }
    
    public void UpdateOfflineDays(int value)
    {
        this.DaysOffline = value;
    }
}


