using CarReservation.Controllers;
using CarReservationRepositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<Data>();
builder.Services.AddScoped<ICarRepository,CarRepository>();
builder.Services.AddTransient<Data>();
builder.Services.AddSingleton<Data>();

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

builder.Services.AddAutoMapper(assemblies);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
