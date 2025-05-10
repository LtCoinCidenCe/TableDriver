using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TableDriver.Models.User.Augmentations
{
  public class UserDisplayNameHistory
  {
    public long ID { get; set; }

    [Column(name: "UserID")]
    public long UserID { get; set; }

    [Column(name: "UserID")]
    [JsonIgnore]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User? User { get; set; }

    [MinLength(2)]
    [MaxLength(60)]
    public string DisplayName { get; set; } = string.Empty;

    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
  }
}
