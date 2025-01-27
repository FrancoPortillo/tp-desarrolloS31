using Data.Contexto;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using Servicios;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowFrontEnd", builder => 
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

app.UseCors("AllowFrontEnd");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddScoped<IEmpresa,EmpresaServicio>();
builder.Services.AddScoped<IEmpresa, EmpleadoServicio>();
builder.Services.AddScoped<IEmpresa, VacacionesServicio>();
builder.Services.AddScoped<IEmpresa, LlegadasTardeServicio>();
builder.Services.AddScoped<IEmpresa, AsistenciaServicio>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar los validadores
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<EmpresaAgregarValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<EmpleadoAgregarValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<VacacionesAgregarValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<LlegadasTardeAgregarValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<AsistenciaAgregarValidador>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
