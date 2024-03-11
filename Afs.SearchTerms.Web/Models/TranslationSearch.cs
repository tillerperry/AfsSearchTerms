using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Afs.SearchTerms.Web.Models;

#nullable disable

[Keyless]
public class TranslationSearch
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Translation { get; set; }
    public string Text { get; set; }
    public string Translated { get; set; }
}