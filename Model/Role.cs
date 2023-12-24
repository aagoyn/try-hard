using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LearningManagement.Model;

[Table("t_role")]
[Index(nameof(RoleCode), IsUnique = true, Name = "role_bk")]
public class Role : BaseModel
{
    [Column("role_name"), MaxLength(20)]
    public string RoleName { get; set; }

    [Column("role_code"), MaxLength(5)]
    public string RoleCode { get; set; }

}
