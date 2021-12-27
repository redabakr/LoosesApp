using Looses.Api.Endpoints;
using Looses.Application;
using Looses.Infrastructure;
using Looses.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddShared();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options => options.CustomSchemaIds(type => type.ToString()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
   
app.UseShared();

app.UseAuthorization();

app.MapCustomerEndpoints();

app.Run();