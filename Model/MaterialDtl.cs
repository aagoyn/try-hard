using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_material_dtl")]
public class MaterialDtl : BaseModel
{
    [Column("material_id")]
    public int MaterialId { get; set; }
    [ForeignKey(nameof(MaterialId))]
    public Material Material { get; set; }

    [Column("material_file")]
    public int MaterialFileId { get; set; }

    [ForeignKey(nameof(MaterialFileId))]
    public FileLms MaterialFile { get; set; }
}
