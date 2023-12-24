using LearningManagement.DBConfig;
using LearningManagement.IRepo;
using LearningManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace LearningManagement.Repo
{
    public class QuestionRepo : IQuestionRepo
    {
        public List<Question> ShowQuestions(DBContextConfig context)
        {
            var questions = context.Questions
                .ToList();
            return questions;

        }
        public Question GetQuestionById(int questionId, DBContextConfig context)
        {
            return context.Questions.FirstOrDefault(q => q.Id == questionId);
        }

        public List<QuestionChoice> GetQuestionChoicesByQuestionId(int questionId, DBContextConfig context)
        {
            var questionChoices = context.QuestionChoices
            .Where(qc => qc.QuestionId == questionId)
            .ToList();

            return questionChoices;

        }

        public List<QuestionChoice> GetQuestionChoiceByQuestionId(int questionId, DBContextConfig context)
        {
            return context.QuestionChoices
                           .Where(qc => qc.QuestionId == questionId)
                           .ToList();
        }

        public QuestionFile? GetQuestionFileByQuestionId(int questionId, DBContextConfig context)
        {
            return context.QuestionFiles
                .Include(qf => qf.FileContent)

                .FirstOrDefault(qf => qf.QuestionId == questionId);
        }

    }

}
