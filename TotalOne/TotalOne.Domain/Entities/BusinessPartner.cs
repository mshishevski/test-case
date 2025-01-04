using System.ComponentModel.DataAnnotations.Schema;

namespace TotalOne.Domain.Entities
{
    public class BusinessPartner
    {
        [Column("businesspartnerid")]
        public int BusinessPartnerId { get; set; }
        [Column("lastupdate")]
        public DateTime LastUpdate { get; set; }
    }
}
