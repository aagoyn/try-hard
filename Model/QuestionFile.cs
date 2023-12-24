using System.ComponentModel.DataAnnotations.Schema;

namespace LearningManagement.Model;

[Table("t_question_file")]
public class QuestionFile : BaseModel
{
    [Column("question_id")]
    public int QuestionId { get; set; }
    [ForeignKey(nameof(QuestionId))]
    public Question Question { get; set; }

    [Column("file_content")]
    public int FileContentId { get; set; }

    [ForeignKey(nameof(FileContentId))]
    public FileLms FileContent { get; set; }
}
