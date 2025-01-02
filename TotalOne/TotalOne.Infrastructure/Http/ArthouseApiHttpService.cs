
using System.Net.Http.Json;

using TotalOne.Application.Services;

namespace TotalOne.Infrastructure.Http;

public class ArthouseApiHttpService : IArthouseApiHttpService
{
    private readonly HttpClient _httpClient;

    public ArthouseApiHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> UpdateBusinessPartner(int businessPartnerId, string Name, CancellationToken cancellationToken)
    {
        var body = new { businessPartnerId, Name };
        var response = await _httpClient.PatchAsJsonAsync("/business-partners", body, cancellationToken);
        return response.IsSuccessStatusCode;
    }
}
