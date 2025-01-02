using Dapper;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using Xunit;

namespace TotalOne.IntegrationTests;

public class IntegrationTestBase : IAsyncLifetime
{
    private const string SearchPath = "inttests";
    private const string IntegrationTestDbConnectionString = $"Host=localhost;Database=postgres;Username=postgres;Password=admin;Search Path={SearchPath};";
    

    protected readonly WebApplicationFactory<Program> _factory;
    protected readonly IServiceScope _scope;
    protected readonly string _connectionString;

    protected IntegrationTestBase()
    {
        _factory = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.BuildServiceProvider(new ServiceProviderOptions()
                {
                    ValidateOnBuild = true,
                    ValidateScopes = true
                });

                services.AddSingleton<NpgsqlConnection>(sp =>
                {
                    var connectionString = IntegrationTestDbConnectionString;
                    return new NpgsqlConnection(connectionString);
                });
            });
        });

        _scope = _factory.Services.CreateScope();

        _connectionString = IntegrationTestDbConnectionString;
    }

    public virtual async Task InitializeAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync($"DROP SCHEMA {SearchPath} CASCADE; CREATE SCHEMA {SearchPath};");
    }

    public virtual async Task DisposeAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await connection.ExecuteAsync($"DROP SCHEMA {SearchPath} CASCADE; CREATE SCHEMA {SearchPath};");
    }

    protected async Task SeedTestDataAsync(Func<NpgsqlConnection, Task> seeder)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        await seeder(connection);
    }

    protected async Task<NpgsqlConnection> AssertionDatabaseContext()
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}