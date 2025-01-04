using System.ComponentModel.DataAnnotations.Schema;

namespace TotalOne.Domain.Entities;

public class Translation
{
    [Column("translationid")]
    public long TranslateionId { get; set; }
    [Column("textcontentid")]
    public long TextContentId { get; set; }
    [Column("languageid")]
    public long LanguageId { get; set; }
    [Column("value")]
    public required string Value { get; set; }
    [Column("lastupdate")]
    public DateTime LastUpdate { get; set; }
}
