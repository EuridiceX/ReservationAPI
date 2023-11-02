using CarReservationRepositories;
using CarReservationWorker;
using CarReservationWorker.Services;
using CarReservationWorkers.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<ICarWorker, CarWorker>();
builder.Services.AddTransient<IReservationWorker, ReservationWorker>();
builder.Services.AddTransient<ICarValidationService, CarValidationService>();
builder.Services.AddTransient<IReservationValidationService, ReservationValidationService>();
builder.Services.AddTransient<IReservationAvailabilityService, ReservationAvailabilityService>();
builder.Services.AddSingleton<Data>();

List<Assembly> assemblies = ConfigureAutoMapper();

builder.Services.AddAutoMapper(assemblies);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

static List<Assembly> ConfigureAutoMapper()
{
    var assemblyNames = new List<AssemblyName>()
{
    new AssemblyName("CarReservationWorkers"),
    new AssemblyName("CarReservationRepositories"),
    new AssemblyName("CarReservationAPI"),
};

    var assemblies = new List<Assembly>();
    foreach (var asembly in assemblyNames)
    {
        assemblies.Add(Assembly.Load(asembly));
    }

    return assemblies;
}