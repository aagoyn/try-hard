using LearningManagement.IService;
using LearningManagement.Model;
using LearningManagement.IRepo;
using LearningManagement.DBConfig;



namespace LearningManagement.Service;
public class LoginService : ILoginService
{
    private IUserRepo userRepo;
    private User loggedInUser;

    public LoginService(IUserRepo userRepo)
    {
        this.userRepo = userRepo;
    }

    public User? Login(string email, string password)
    {
        using (var context = new DBContextConfig())
        {
            return userRepo.GetUserByEmailPass(email, password, context);
        }
    }

    public void SetLoggedInUser(User user)
    {
        loggedInUser = user;
    }

    public User GetLoggedInUser()
    {
        return loggedInUser;
    }
}