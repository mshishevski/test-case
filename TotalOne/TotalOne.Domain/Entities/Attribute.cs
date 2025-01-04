using System.ComponentModel.DataAnnotations.Schema;

namespace TotalOne.Domain.Entities;

public class Attribute
{
    [Column("attributeid")]
    public long AttributeId { get; set; }
    [Column("type")]
    public required string Type { get; set; }
    [Column("key")]
    public required string Key { get; set; }
    [Column("textcontentid")]
    public long TextContentId { get; set; }
    [Column("iscustom")]
    public bool IsCustom { get; set; }
    [Column("issecret")]
    public bool IsSecret { get; set; }
    [Column("lastupdate")]
    public DateTime LastUpdate { get; set; }
}
