using LearningManagement.IRepo;
using LearningManagement.Repo;
using LearningManagement.Service;
using LearningManagement.IService;
using LearningManagement.View;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LearningManagement.Helper;
using LearningManagement.Model;


namespace LearningManagement.Config;

class DIConfig
{
    public static IHost Init()
    {
        var builder = Host.CreateApplicationBuilder();

        //init repo
        builder.Services.AddSingleton<IAssignmentRepo, AssignmentRepo>();
        builder.Services.AddSingleton<IAssignmentDtlRepo, AssignmentDtlRepo>();
        builder.Services.AddSingleton<IClassEnrollmentRepo, ClassEnrollmentRepo>();
        builder.Services.AddSingleton<IClassRepo, ClassRepo>();
        builder.Services.AddSingleton<IFileRepo, FileRepo>();
        builder.Services.AddSingleton<IForumRepo, ForumRepo>();
        builder.Services.AddSingleton<ILearningRepo, LearningRepo>();
        builder.Services.AddSingleton<IMaterialRepo, MaterialRepo>();
        builder.Services.AddSingleton<IMaterialDtlRepo, MaterialDtlRepo>();
        builder.Services.AddSingleton<IQuestionRepo, QuestionRepo>();
        builder.Services.AddSingleton<ISessionRepo, SessionRepo>();
        builder.Services.AddSingleton<ISubmissionRepo, SubmissionRepo>();
        builder.Services.AddSingleton<IUserRepo, UserRepo>();


        //init service
        builder.Services.AddSingleton<IAssignmentService, AssignmentService>();
        builder.Services.AddSingleton<IClassService, ClassService>();
        builder.Services.AddSingleton<IFileService, FileService>();
        builder.Services.AddSingleton<IForumService, ForumService>();
        builder.Services.AddSingleton<ILearningService, LearningService>();
        builder.Services.AddSingleton<ILoginService, LoginService>();
        builder.Services.AddSingleton<IMaterialDtlService, MaterialDtlService>();
        builder.Services.AddSingleton<IMaterialService, MaterialService>();
        builder.Services.AddSingleton<ISessionService, SessionService>();
        builder.Services.AddSingleton<ISubmissionService, SubmissionService>();
        builder.Services.AddSingleton<IUserService, UserService>();

        //init view
        builder.Services.AddSingleton<LecturerView>();
        builder.Services.AddSingleton<MainView>();
        builder.Services.AddSingleton<StudentView>();
        builder.Services.AddSingleton<SuperAdminView>();

        //init addons 
        builder.Services.AddSingleton<User>();
        builder.Services.AddSingleton<SessionHelper>();
        builder.Services.AddSingleton<QuestionAnswer>();

        return builder.Build();
    }
}
