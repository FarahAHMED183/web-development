namespace CRUD_Operation.AppMetaData.BaseRouter
{
    public partial class Router
    {
        public class CourseRouter : Router
        {   
            public const string Prefix = Rule + "Course";
            public const string GetAll = Prefix+"/";
            public const string GetById = Prefix+"/{id}";
            public const string Create = Prefix+"/";
            public const string Update = Prefix+"/{id}";
            public const string Delete = Prefix+"/{id}";
            public const string Search = Prefix + "/search";
    }
    }
}
