namespace TotalOne.Api.Requests;

public class UpdateBusinessPartnerRequest
{
    public long BusinessPartnerId { get; set; }
    public required string Name { get; set; }
}
