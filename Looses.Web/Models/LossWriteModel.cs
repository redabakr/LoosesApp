using System.ComponentModel.DataAnnotations;

namespace Looses.Web.Models;

public class LossWriteModel
{
    
    [Required]
    [MaxLength(50)]
    public string WellName { get; set; }
    [Required]
    [MaxLength(100)]
    public string EventName { get; set; }

    [Required] public DateTime LoosDate { get; set; } = DateTime.Now;

}