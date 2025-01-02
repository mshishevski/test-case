using TotalOne.Api.Dependencies;
using TotalOne.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new SlugifyRouteConvention());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTotalOneServices();

var arthouseBaseAddress = builder.Configuration.GetValue<string>("ExternalIntegrations:ArthouseBaseUrl");

builder.Services.AddExternalServices(arthouseBaseAddress!);

builder.Services.AddGraphQlQueriesAndMutations();
    
var app = builder.Build();

app.MapGraphQL("/graphql");

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

public partial class Program { }
