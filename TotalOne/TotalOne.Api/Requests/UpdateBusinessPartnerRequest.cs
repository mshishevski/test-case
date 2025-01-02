namespace TotalOne.Api.Requests;

public class UpdateBusinessPartnerRequest
{
    public int BusinessPartnerId { get; set; }
    public required string Name { get; set; }
}
