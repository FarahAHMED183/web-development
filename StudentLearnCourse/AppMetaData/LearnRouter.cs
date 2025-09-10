namespace CRUD_Operation.AppMetaData.BaseRouter
{
    public partial class Router
    {
        public const string LearnController = Rule + "Learn";
        public const string GetAllEnrollments = "GetAllEnrollments";
        public const string GetEnrollmentsByStudent = "GetByStudent/{studentId}";
        public const string GetEnrollmentsByCourse = "GetByCourse/{courseId}";
        public const string EnrollStudent = "Enroll";
        public const string UnenrollStudent = "Unenroll";
        public const string UpdateGrade = "UpdateGrade";
    }
}
