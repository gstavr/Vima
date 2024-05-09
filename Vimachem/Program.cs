using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Vimachem.BackgroundServices;
using Vimachem.Data;
using Vimachem.Filters;
using Vimachem.Mapping;
using Vimachem.OpenApi;
using Vimachem.Repository;
using Vimachem.Repository.IRepository;
using WebApiEntityFrameworkDockerSqlServer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

// Services DI
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Register the LoggingActionFilter as a scoped service
builder.Services.AddScoped<LoggingActionFilter>();

builder.Services.AddHostedService<AuditLogCleanupService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(LoggingActionFilter));
});

builder.Services.AddLogging();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: Uncomment for Versioning and comment the Default Controllers that i have
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
    //options.ApiVersionReader = new UrlSegmentApiVersionReader();
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-ApiVersion"));
})
    .AddMvc()
    .AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

var app = builder.Build();

app.UseSwagger();

// TODO: Comment this and uncomment the bellow if you want to see versioning
//app.UseSwaggerUI();

app.UseSwaggerUI(options =>
{
    IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();
    foreach (var apiVersionDescription in descriptions)
    {
        string url = $"/swagger/{apiVersionDescription.GroupName}/swagger.json";
        string name = apiVersionDescription.GroupName.ToUpperInvariant();

        options.SwaggerEndpoint(url, name);
    }

    if (!app.Environment.IsDevelopment())
    {
        options.RoutePrefix = "";
    }

});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Run Migrations to the Database
Migrations.RunMigrations(app);

app.Run();


