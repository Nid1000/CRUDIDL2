using DbModel.demoDb;
using Microsoft.EntityFrameworkCore;
using Mvc.Bussnies.Casa;
using Mvc.Bussnies.Mascota;
using Mvc.Bussnies.Persona;
using Mvc.Repository.CasaRepo.Contratos;
using Mvc.Repository.CasaRepo.Implementacion;
using Mvc.Repository.MascotaRepo.Contratos;
using Mvc.Repository.MascotaRepo.Implementacion;
using Mvc.Repository.MascotaTipoRepo.Contratos;
using Mvc.Repository.MascotaTipoRepo.Implementacion;
using Mvc.Repository.PersonaRepo.Contratos;
using Mvc.Repository.PersonaRepo.Implementacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<_demoContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("demoDb");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Inyección de dependencias - Repositories
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<ICasaRepository, CasaRepository>();
builder.Services.AddScoped<IMascotaTipoRepository, MascotaTipoRepository>();
builder.Services.AddScoped<IMascotaRepository, MascotaRepository>();

// Inyección de dependencias - Business Logic
builder.Services.AddScoped<IPersonaBussnies, PersonaBussnies>();
builder.Services.AddScoped<ICasaBussnies, CasaBussnies>();
builder.Services.AddScoped<IMascotaTipoBussnies, MascotaTipoBussnies>();
builder.Services.AddScoped<IMascotaBussnies, MascotaBussnies>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<_demoContext>();
    await db.Database.OpenConnectionAsync();
    await db.Database.CloseConnectionAsync();

    // Seed mínimo para evitar error de FK al crear Persona
    var tipos = await db.PersonaTipoDocumento.AsNoTracking().ToListAsync();
    if (!tipos.Any(t => t.Id == 1))
    {
        db.PersonaTipoDocumento.Add(new PersonaTipoDocumento
        {
            Id = 1,
            Codigo = "DNI",
            Descripcion = "DNI",
            UserCreate = 1
        });
    }
    if (!tipos.Any(t => t.Id == 2))
    {
        db.PersonaTipoDocumento.Add(new PersonaTipoDocumento
        {
            Id = 2,
            Codigo = "CE",
            Descripcion = "Carnet de extranjería",
            UserCreate = 1
        });
    }
    if (db.ChangeTracker.HasChanges())
    {
        await db.SaveChangesAsync();
    }

    // Seed básico MascotaTipo
    var tiposMascota = await db.MascotaTipo.AsNoTracking().ToListAsync();
    if (!tiposMascota.Any(t => t.Codigo == "PERRO"))
    {
        db.MascotaTipo.Add(new MascotaTipo { Codigo = "PERRO", Descripcion = "Perro", UserCreate = 1 });
    }
    if (!tiposMascota.Any(t => t.Codigo == "GATO"))
    {
        db.MascotaTipo.Add(new MascotaTipo { Codigo = "GATO", Descripcion = "Gato", UserCreate = 1 });
    }
    if (db.ChangeTracker.HasChanges())
    {
        await db.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
