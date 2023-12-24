using LearningManagement.Model;

namespace LearningManagement.Helper;


public class QuestionAnswer : BaseModel
{
    public int QuestionId { get; set; }
    public string? TextAnswer { get; set; }
    public bool IsFile { get; set; }
    public bool IsChoice { get; set; }
    public int? ChoiceId { get; set; }
    public string? FileTitle { get; set; }
    public string? FileExtension { get; set; }
}

