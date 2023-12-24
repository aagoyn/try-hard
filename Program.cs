using LearningManagement.Config;
using LearningManagement.View;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
namespace LearningManagement
{
    class Program
    {
        static void Main()
        {
            var host = DIConfig.Init();
            var app = host.Services.GetService<MainView>();
            app.Welcome();
        }

    }
}
