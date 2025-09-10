
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var instance = DBConnectionSingleton.Instance;
var connectionString = instance.ConnectionString;



    builder.Services.AddDbContext<ApplicationDbContext>(options => options
        .UseSqlServer(connectionString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        .UseLazyLoadingProxies()
    );


// ignore loop reference
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);






// add repository and it interface
builder.Services.AddRepositories();




// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/Error/{0}");


app.UseCors(c =>
{
    c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();


app.MapControllers();

app.UseStaticFiles();

// update database
var scope = app.Services.CreateScope();
using var AppDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var Logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);


try
{
    await AppDbContext.Database.MigrateAsync();

}
catch (Exception ex)
{
    Logger.LogError(ex, ex.Message);

}


app.Run();
