using OoberEats.Api.Interfaces;
using OoberEats.Application;
using OoberEats.Infrastucture;
using System.Reflection;

// Configure Web app builder and services
var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    // Registering services from custom projects
    builder.Services
        .AddApplication()
        .AddInfrastructure();
}


// Configure middlewares
var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // Register all the minimal api modules dynamically
    var modules = Assembly.GetExecutingAssembly()
                          .GetTypes()
                          .Where(t => typeof(IMinimalApiModule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

    foreach (var module in modules)
    {
        var instance = Activator.CreateInstance(module) as IMinimalApiModule;
        instance?.AddRoutes(app);
    }

    app.Run();
}
