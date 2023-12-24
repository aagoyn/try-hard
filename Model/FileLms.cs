using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LearningManagement.Model;

[Table("t_file")]
public class FileLms : BaseModel
{
    [Column("file_title", TypeName = "TEXT")]
    public string FileTitle { get; set; }

    [Column("file_extension"), MaxLength(5)]
    public string FileExtension { get; set; }
}
