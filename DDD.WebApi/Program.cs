using DDD.Infrastructure;
using DDD.Infrastructure.Persistence;
using DDD.WebApi;
using DDD.Application;
using DDD.WebApi.Filters;
using DDD.WebApi.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();

    // Initialise and seed database
    using var scope = app.Services.CreateScope();
    var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    await initializer.InitializeAsync();
    await initializer.SeedAsync();
}
else
{
    app.UseHsts();
}

//app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseRouting();
app.UseExceptionFilter();
app.MapCustomerEndpoints();
app.Run();
