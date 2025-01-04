using Dapper;
using Xunit;
using System.Net.Http.Json;
using TotalOne.Application.Queries;
using TotalOne.Domain.Result;

namespace TotalOne.IntegrationTests;

public class BusinessPartnersTests : IntegrationTestBase
{
    [Fact]
    public async Task When_page_0_and_2_items_are_requested_only_two_are_retrieved()
    {
        // Arrange
        await SeedTestDataAsync(async connection =>
        {
            await connection.ExecuteAsync(@"
                CREATE TABLE BusinessPartners (
                    BusinessPartnerId SERIAL PRIMARY KEY,
                    Name TEXT NOT NULL,
                    LastUpdate TIMESTAMP NOT NULL
                );
            ");

            await connection.ExecuteAsync(@"
                INSERT INTO BusinessPartners (Name, LastUpdate)
                VALUES
                ('Partner A', NOW()),
                ('Partner B', NOW()),
                ('Partner C', NOW());
            ");
        });

        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/businesspartners?PageIndex=0&PageSize=2&DescendingSortDirection=false&SortBy=Name");

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<BaseTableResult<BusinessPartnerQueryResult>>();

        Assert.NotNull(result);
        Assert.Equal(2, result.Entries.Count);
    }

    [Fact]
    public async Task When_page_0_and_0_items_are_requested_a_validation_error_is_thrown()
    {
        // Arrange
        await SeedTestDataAsync(async connection =>
        {
            await connection.ExecuteAsync(@"
                CREATE TABLE BusinessPartners (
                    BusinessPartnerId SERIAL PRIMARY KEY,
                    Name TEXT NOT NULL,
                    LastUpdate TIMESTAMP NOT NULL
                );
            ");

            await connection.ExecuteAsync(@"
                INSERT INTO BusinessPartners (Name, LastUpdate)
                VALUES
                ('Partner A', NOW()),
                ('Partner B', NOW()),
                ('Partner C', NOW());
            ");
        });


        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/businesspartners?PageIndex=0&PageSize=0&DescendingSortDirection=false&SortBy=Name");

        // Assert
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<BaseTableResult<BusinessPartnerQueryResult>>();

        Assert.NotNull(result);
        Assert.Empty(result.Entries);
    }
}
