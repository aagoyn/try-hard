using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_class_enrollment")]
[Index(nameof(ClassId), nameof(StudentId), IsUnique = true, Name = "class_enrollment_ck")]
public class ClassEnrollment : BaseModel
{
    [Column("class_id")]
    public int ClassId { get; set; }

    [ForeignKey(nameof(ClassId))]
    public Class Class { get; set; }

    [Column("student_id")]
    public int StudentId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public User Student { get; set; }
}
