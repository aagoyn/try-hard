using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.DBConnection
{
    public class ConnectionDB
    {
        public const string Host = "localhost";
        public const string Database = "lms";
        public const string Username = "postgres";
        public const string Password = "1105";

        public static string ConnectionString => $"Host={Host};Database={Database};User ID={Username};Password={Password}";
    }
}
