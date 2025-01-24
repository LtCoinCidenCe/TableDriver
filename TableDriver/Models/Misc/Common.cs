using System.ComponentModel.DataAnnotations.Schema;

namespace TableDriver.Models.Misc;

// this becomes a requirement and doesn't implicitly does anything
public interface Common
{
    public ulong ID { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LastUpdatedAt { get; set; }
}
