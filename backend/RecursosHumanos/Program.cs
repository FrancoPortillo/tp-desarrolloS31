using Data.Contexto;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Servicios.Servicios;
using Servicios.Validadores;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
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
builder.Services.AddScoped<IPermisoAusencia, PermisoAusenciaServicio>();
builder.Services.AddScoped<IDocumentacion, DocumentacionServicio>();
builder.Services.AddScoped<IInasistencia, InasistenciaServicio>();
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
builder.Services.AddValidatorsFromAssemblyContaining<DocumentacionAgregarValidador>();
builder.Services.AddValidatorsFromAssemblyContaining<PermisoAgregarValidador>();

// Configurar el contexto de la base de datos
builder.Services.AddDbContext<BdRrhhContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("RecursosHumanos")));

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
app.UseStaticFiles(); 
app.UseAuthorization();
app.UseCors("AllowFrontEnd");

app.MapControllers();

app.Run();