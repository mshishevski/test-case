using Dapper;

using Xunit;

namespace TotalOne.IntegrationTests;

public class DataContextTests : IntegrationTestBase
{
    [Fact]
    public async Task Ensure_database_and_tables_are_created()
    {
        // Arrange
        await SeedTestDataAsync(async connection =>
        {
            await connection.ExecuteAsync(@"
                CREATE TABLE test_table (
                    test_table_id SERIAL PRIMARY KEY,
                    name TEXT NOT NULL
                );

                INSERT INTO test_table (name)
                    VALUES
                    ('test 1'),
                    ('test 2');
            ");
        });

        // Act
        using var connection = await AssertionDatabaseContext();

        // Assert
        var schemaExists = await connection.ExecuteScalarAsync<bool>(@"
            SELECT EXISTS (
                SELECT 1
                FROM information_schema.schemata
                WHERE schema_name = 'inttests'
            );
        ");
        Assert.True(schemaExists, "Schema does not exist.");

        var tableExists = await connection.ExecuteScalarAsync<bool>(@"
            SELECT EXISTS (
                SELECT 1
                FROM information_schema.tables
                WHERE table_schema = 'inttests' AND table_name = 'test_table'
            );
        ");
        Assert.True(tableExists, "Table 'test_table' does not exist.");

        var testData = await connection.QueryAsync<string>("SELECT name FROM test_table");
        var testDataList = testData.ToList();

        Assert.Equal(2, testDataList.Count); 
        Assert.Contains("test 1", testDataList);
        Assert.Contains("test 2", testDataList);
    }
}
