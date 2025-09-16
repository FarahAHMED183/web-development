namespace CRUD_Operation.AppMetaData.BaseRouter
{
    public partial class Router
    {
        public class StudentRouter : Router
        {
            public const string Prefix = Rule + "Student";
            public const string GetAllStudents = Prefix + "/";
            public const string GetStudentById = Prefix + "/{id}";
            public const string CreateStudent = Prefix + "/";
            public const string UpdateStudent = Prefix + "/{id}";
            public const string DeleteStudent = Prefix + "/{id}";
            public const string SearchStudents = Prefix + "/search";
            
        }
        
    }
}
