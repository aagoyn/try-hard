using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_question")]
public class Question : BaseModel
{
    [Column("question_content", TypeName = "TEXT")]
    public string QuestionContent { get; set; }

    [Column("question_type"), MaxLength(30)]
    public string QuestionType { get; set; }
}
