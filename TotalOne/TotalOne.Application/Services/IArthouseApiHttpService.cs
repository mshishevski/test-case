namespace TotalOne.Application.Services;

public interface IArthouseApiHttpService
{
    Task<bool> UpdateBusinessPartner(int businessPartnerId, string Name, CancellationToken cancellationToken);
}
