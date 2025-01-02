using Microsoft.Extensions.Configuration;

using Npgsql;
using System.Data;

using TotalOne.Application;

namespace TotalOne.Infrastructure;

public class TotalOneContext : ITotalOneContext
{
    private readonly IConfiguration _configuration;

    public TotalOneContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection() => 
        new NpgsqlConnection(_configuration.GetConnectionString("TotalOneContext"));
}
