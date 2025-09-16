namespace CRUD_Operation.Repositories
{
    public static class RepositoryDependencies
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //Configuration Of Automapper

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // Register generic repository for all entities
            services.AddScoped(typeof(Repositories.Interfaces.IGenericRepository<>), typeof(Implementations.GenericRepository<>));
            
            // Register specific repositories
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ILearnRepository, LearnRepository>();
            
            return services;
        }
    }
}
