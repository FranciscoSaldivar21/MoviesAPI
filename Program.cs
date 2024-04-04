using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using MoviesAPI;
using MoviesAPI.Endpoints;
using MoviesAPI.Migrations;
using MoviesAPI.Models;
using MoviesAPI.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//Area de servicios
//Conexion a la base de datos
//DefaultConnection proviene del archivo appsettings development
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer("name=DefaultConnection"));

builder.Services.AddCors(opc => opc.AddDefaultPolicy(config => config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())); //CORS

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyección de servicio
builder.Services.AddScoped<IRepositoryGenders, RepositoryGenders>();

builder.Services.AddOutputCache();   

//Fin de area de servicios
var app = builder.Build();
//Inicio de area de middlewares

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();
app.UseOutputCache();

app.MapGet("/", () => "Api de Francisco Saldivar");

//MapGroup 
var gendersEndPoint = app.MapGroup("/genders").MapGenders();


//Fin de middlewares
app.Run();

