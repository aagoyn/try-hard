using LearningManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace LearningManagement.Model;

[Table("t_user")]
[Index(nameof(Email), IsUnique = true, Name = "user_email_bk")]
public class User : BaseModel
{
    [Column("email"), MaxLength(30)]
    public string Email { get; set; }

    [Column("fullname"), MaxLength(100)]
    public string Fullname { get; set; }

    [Column("pwd", TypeName = "TEXT")]
    public string Password { get; set; }

    [Column("user_role")]
    public int RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; }

    [Column("photo_profile")]
    public int? PhotoProfileId { get; set; }

    [ForeignKey(nameof(PhotoProfileId))]
    public FileLms? PhotoProfile { get; set; }
}

