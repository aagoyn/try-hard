using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_question_choice")]
public class QuestionChoice : BaseModel
{
    [Column("question_id")]
    public int QuestionId { get; set; }

    [ForeignKey(nameof(QuestionId))]
    public virtual Question Question { get; set; }

    [Column("option_abc", TypeName = "Char(1)")]
    public string OptionAbc { get; set; }

    [Column("option_content", TypeName = "TEXT")]
    public string OptionContent { get; set; }

    [Column("is_correct")]
    public bool IsCorrect { get; set; }

    [NotMapped]
    public virtual List<QuestionChoice> Choices { get; set; }
}
