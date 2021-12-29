using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Looses.Web.Models;
public class LossReadModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("wellName")]
    public string WellName { get; set; }
    [JsonPropertyName("eventName")]
    public string EventName { get; set; }
    [JsonPropertyName("loosDate")]
    [DisplayFormat(DataFormatString = "d")]
    public DateTime LoosDate { get; set; }
    [JsonPropertyName("daysOffline")]
    public int DaysOffline { get; set; }
}

//public record LossReadModel(int Id, string WellName,string EventName,DateTime LoosDate,int DaysOffline);