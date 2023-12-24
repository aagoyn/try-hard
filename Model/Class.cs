using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_class")]
[Index(nameof(ClassCode), IsUnique = true, Name = "class_bk")]
public class Class : BaseModel
{
    [Column("class_code"), MaxLength(7)]
    public string ClassCode { get; set; }

    [Column("class_name"), MaxLength(30)]
    public string ClassName { get; set; }

    [Column("class_desc", TypeName = "TEXT")]
    public string ClassDesc { get; set; }

    [Column("class_photo")]
    public int ClassPhotoId { get; set; }
    [ForeignKey(nameof(ClassPhotoId))]
    public FileLms? ClassPhoto { get; set; }

    [Column("lecturer_id")]
    public int LecturerId { get; set; }

    [ForeignKey(nameof(LecturerId))]
    public User Lecturer { get; set; }
}
