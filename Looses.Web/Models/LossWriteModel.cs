using System.ComponentModel.DataAnnotations;

namespace Looses.Web.Models;

public class LossWriteModel
{
    public LossWriteModel()
    {
        Key = new Random().Next(1, 100);
    }
    public int Key { get; set; }  
    [Required]
    [MaxLength(50)]
    public string WellName { get; set; }
    [Required]
    [MaxLength(100)]
    public string EventName { get; set; }
    [DisplayFormat(DataFormatString = "d")]
    [Required] public DateTime LoosDate { get; set; } = DateTime.Now;

}