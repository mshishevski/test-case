namespace TotalOne.Api.Requests
{
    public class FilterBusinessPartnersRequest
    {
        public long BusinessPartnerId { get; set; }
        public DateTime? LastUpdateStart { get; set; }
        public DateTime? LastUpdateEnd { get; set; }
        public string? Name { get; set; }
    }
}
