using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using LearningManagement.Model;
using LearningManagement.Helper;

namespace LearningManagement.DBConfig;

public class DBContextConfig : DbContext
{
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<AssignmentDtl> AssignmentDtls { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassEnrollment> ClassEnrollments { get; set; }
    public DbSet<FileLms> LmsFiles { get; set; }
    public DbSet<Forum> Forums { get; set; }
    public DbSet<ForumDtl> ForumDtls { get; set; }
    public DbSet<Learning> Learnings { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<MaterialDtl> MaterialDtls { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionChoice> QuestionChoices { get; set; }
    public DbSet<QuestionFile> QuestionFiles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<SubmissionDtl> SubmissionDtls { get; set; }
    public DbSet<SubmissionDtlFile> SubmissionDtlFiles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<QuestionAnswer> answers { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string Host = "localhost";
        const string Database = "lms";
        const string Username = "postgres";
        const string Password = "1105";

        const string ConnectionString = $"Host={Host};Database={Database};" +
                              $"Username={Username};Password={Password}";


        optionsBuilder.UseNpgsql(ConnectionString);
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }
}
