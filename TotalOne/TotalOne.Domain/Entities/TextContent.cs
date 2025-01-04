using System.ComponentModel.DataAnnotations.Schema;

namespace TotalOne.Domain.Entities;

public class TextContent
{
    [Column("textcontentid")]
    public long TextContentId { get; set; }
    [Column("originaltext")]
    public required string OriginalText { get; set; }
    [Column("original_language_id")]
    public long originallanguageid { get; set; }
    [Column("lastupdate")]
    public DateTime LastUpdate { get; set; }
}
