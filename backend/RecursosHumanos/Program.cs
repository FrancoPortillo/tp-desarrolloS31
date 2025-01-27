using Data.Contexto;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Servicios.Validadores;
using Servicios.Servicios;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowFrontEnd", builder => 
    {
        builder.WithOrigins("http://localhost:5432")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddScoped<IEmpresa, EmpresaServicio>();
builder.Services.AddScoped<IEmpleado, EmpleadoServicio>();
builder.Services.AddScoped<IVacaciones, VacacionesServicio>();
builder.Services.AddScoped<ILlegadaTarde, LlegadaTardeServicio>();
builder.Services.AddScoped<IAsistencia, AsistenciaServicio>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar los validadores
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<EmpresaAgregarValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<EmpleadoAgregarValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<VacacionesAgregarValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<LLegadaTardeAgregarValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<AsistenciaAgregarValidador>();

// Configurar el contexto de la base de datos
builder.Services.AddDbContext<BdRrhhContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowFrontEnd");

app.MapControllers();

app.Run();