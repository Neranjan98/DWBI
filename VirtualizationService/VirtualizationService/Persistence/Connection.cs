using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualizationService.Persistence
{
    [Table("tbConnection")]
    public record Connection([property: Key] string? ConnectionType, string? ConnectionString);
}
