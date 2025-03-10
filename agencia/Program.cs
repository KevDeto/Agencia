using agencia;
using agencia.Context;
using agencia.Models.Repository;
using agencia.Models.Repository.Interfaces;
using agencia.Services;
using agencia.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Create a variable for the connection string
var connectionString = builder.Configuration.GetConnectionString("Connection");
// Register a service for connection
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();

//Set up mapper
//var mapperConfig = new MapperConfiguration(m =>
//{
//    m.AddProfile(new MappingProfile());
//});
//IMapper mapper = mapperConfig.CreateMapper();
//builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMvc();

//repositoryes
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();

//Set up swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();//error

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
