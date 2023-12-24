using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_material")]
public class Material : BaseModel
{
    [Column("material_title"), MaxLength(100)]
    public string MaterialTitle { get; set; }

    [Column("material_content", TypeName = "TEXT")]
    public string MaterialContent { get; set; }

    [Column("session_id")]
    public int SessionId { get; set; }

    [ForeignKey(nameof(SessionId))]
    public Session Session { get; set; }
}
