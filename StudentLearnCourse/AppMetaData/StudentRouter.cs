namespace CRUD_Operation.AppMetaData.BaseRouter
{
    public partial class Router
    {
        public const string StudentController = Rule + "Student";
        public const string GetAllStudents = "GetAll";
        public const string GetStudentById = "GetById/{id}";
        public const string CreateStudent = "Create";
        public const string UpdateStudent = "Update/{id}";
        public const string DeleteStudent = "Delete/{id}";
    }
}
