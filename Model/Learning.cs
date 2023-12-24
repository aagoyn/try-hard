
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace LearningManagement.Model;

[Table("t_learning")]
public class Learning : BaseModel
{
    [Column("day_name"), MaxLength(10)]
    public string DayName { get; set; }

    [Column("learning_date")]
    public DateOnly LearningDate { get; set; }
    
    [Column("class_id")]
    public int ClassId { get; set; }
    
    [ForeignKey(nameof(ClassId))]
    public Class Class { get; set; }
}
