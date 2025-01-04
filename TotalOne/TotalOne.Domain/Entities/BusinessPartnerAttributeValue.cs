using System.ComponentModel.DataAnnotations.Schema;

namespace TotalOne.Domain.Entities;

public class BusinessPartnerAttributeValue
{
    [Column("businesspartnerattributevalueid")]
    public long BusinessPartnerAttributeValueId { get; set; }
    [Column("businesspartnerid")]
    public long BusinessPartnerId { get; set; }
    [Column("attributeid")]
    public int AttributeId { get; set; }
    [Column("textcontentid")]
    public int TextContentId { get; set; }
    [Column("lastupdate")]
    public DateTime LastUpdate { get; set; }

}
