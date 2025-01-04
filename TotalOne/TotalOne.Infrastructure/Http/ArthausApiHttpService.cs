
using System.Net.Http.Json;

using TotalOne.Application.Services;

namespace TotalOne.Infrastructure.Http;

public class ArthausApiHttpService : IArthausApiHttpService
{
    private readonly HttpClient _httpClient;

    public ArthausApiHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> UpdateBusinessPartner(long businessPartnerId, string Name, CancellationToken cancellationToken)
    {
        var body = new { businessPartnerId, Name };
        var response = await _httpClient.PatchAsJsonAsync("/business-partners", body, cancellationToken);
        return response.IsSuccessStatusCode;
    }
}
