namespace CRUD_Operation.AppMetaData.BaseRouter
{
    public partial class Router
    {
        public class LearnRouter : Router
        {
        public const string Prefix = Rule + "Learn";
        public const string GetAllEnrollments = Prefix + "/";
        public const string GetEnrollmentsByStudent = Prefix + "/{id}";
        public const string GetEnrollmentsByCourse = Prefix + "/{id}";
        public const string EnrollStudent = Prefix + "/";
        public const string UnenrollStudent = Prefix + "/";
        public const string UpdateGrade = Prefix + "/{id}";
    }
}}