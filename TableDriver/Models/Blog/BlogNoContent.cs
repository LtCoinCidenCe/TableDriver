using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TableDriver.Models.Misc;

namespace TableDriver.Models.Blog
{
    public class BlogNoContent : Common
    {
        public long ID { get; set; }

        [Column(name: "authorid")]
        [InverseProperty("Blogs")]
        [JsonIgnore]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User.User? Author { get; set; }

        [Column(name: "authorid")]
        public long AuthorID { get; set; }

        [MaxLength(60)]
        public string Title { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
