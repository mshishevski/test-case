namespace TotalOne.Application.Services;

public interface IArthausApiHttpService
{
    Task<bool> UpdateBusinessPartner(long businessPartnerId, string Name, CancellationToken cancellationToken);
}
