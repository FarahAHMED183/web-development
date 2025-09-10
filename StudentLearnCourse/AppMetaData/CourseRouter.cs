namespace CRUD_Operation.AppMetaData.BaseRouter
{
    public partial class Router
    {
        public const string CourseController = Rule + "Course";
        public const string GetAllCourses = "GetAll";
        public const string GetCourseById = "GetById/{id}";
        public const string CreateCourse = "Create";
        public const string UpdateCourse = "Update/{id}";
        public const string DeleteCourse = "Delete/{id}";
        public const string SearchCourses = "Search";
    }
}
