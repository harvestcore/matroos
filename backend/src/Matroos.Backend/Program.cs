using AppContext.Services;
using AppContext.Services.Interfaces;

using Matroos.Backend.Services;
using Matroos.Backend.Services.Interfaces;
using Matroos.Resources.Classes.Mappers;
using Matroos.Resources.Exceptions;

using BBackgroundService = Matroos.Backend.Services.BackgroundService;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
IConfiguration c = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json", false, true)
    .Build();
IConfigurationService cs = new ConfigurationService(c);
IDataContextService dcs = new DataContextService(cs);

builder.Services.AddSingleton<IConfigurationService>(cs);
builder.Services.AddSingleton<IDataContextService>(dcs);

builder.Services.AddResourcesServices();
builder.Services.AddHostedService<BBackgroundService>();
builder.Services.AddSingleton<ICommunicationService, CommunicationService>();
builder.Services.AddSingleton<IBotsService, BotsService>();
builder.Services.AddSingleton<IUserCommandsService, UserCommandsService>();
builder.Services.AddSingleton<IWorkersService, WorkersService>();
builder.Services.AddSingleton<ICommunicationService, CommunicationService>();

// Mappers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonMapper(dcs));
});
DBMapper.RegisterClassMappers(dcs);

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000");
    });
});

// Build the application.
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // CORS
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
