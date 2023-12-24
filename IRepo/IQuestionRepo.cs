using LearningManagement.DBConfig;
using LearningManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.IRepo
{
    public interface IQuestionRepo
    {
        List<Question> ShowQuestions(DBContextConfig context);
        List<QuestionChoice> GetQuestionChoicesByQuestionId(int questionId, DBContextConfig context);
        List<QuestionChoice> GetQuestionChoiceByQuestionId(int questionId, DBContextConfig context);
        QuestionFile? GetQuestionFileByQuestionId(int questionId, DBContextConfig context);
        Question GetQuestionById(int questionId, DBContextConfig context);
    }
}
