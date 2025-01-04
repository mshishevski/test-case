using System.ComponentModel.DataAnnotations.Schema;

namespace TotalOne.Domain.Entities;

public class Language
{
    [Column("languageid")]
    public long LanguageId { get; set; }
    [Column("locale")]
    public required string Locale { get; set; }
    [Column("nameoriginal")]
    public required string OriginalName { get; set; }
    [Column("textcontentid")]
    public long TextContentId { get; set; }
    [Column("flag")]
    public required string Flag { get; set; }
    [Column("lastupdate")]
    public DateTime LastUpdate { get; set; }
}
