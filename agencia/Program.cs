using agencia;
using agencia.Context;
using agencia.Models.Repository;
using agencia.Models.Repository.Interfaces;
using agencia.Services;
using agencia.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigureDatabase(builder);
ConfigureAutoMapper(builder);
ConfigureRepositories(builder);
ConfigureSwagger(builder);
ConfigureMvc(builder);

var app = builder.Build();

ConfigureMiddleware(app);

app.Run();

void ConfigureDatabase(WebApplicationBuilder builder)
{
    // Add services to the container.
    // Create a variable for the connection string
    var connectionString = builder.Configuration.GetConnectionString("Connection");
    // Register a service for connection
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
}

void ConfigureAutoMapper(WebApplicationBuilder builder)
{
    // Register the AutoMapper service
    builder.Services.AddAutoMapper(typeof(MappingProfile));
}

void ConfigureRepositories(WebApplicationBuilder builder)
{
    // Register the repository services
    builder.Services.AddScoped<IClientRepository, ClientRepository>();
    builder.Services.AddScoped<IClientService, ClientService>();
    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
    builder.Services.AddScoped<IServiceService, ServiceService>();
    builder.Services.AddScoped<IPackageRepository, PackageRepository>();
    builder.Services.AddScoped<IPackageService, PackageService>();
    builder.Services.AddScoped<ISaleRepository, SaleRepository>();
    builder.Services.AddScoped<ISaleService, SaleService>();
}

void ConfigureSwagger(WebApplicationBuilder builder)
{
    // Register the Swagger services
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddOpenApi();
}

void ConfigureMvc(WebApplicationBuilder builder)
{
    // Register the MVC services
    builder.Services.AddControllers();
    builder.Services.AddMvc();
}

void ConfigureMiddleware(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Agencia-API"));
    }
    //Middleware configuration
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
