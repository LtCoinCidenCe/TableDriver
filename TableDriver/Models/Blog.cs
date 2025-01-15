using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TableDriver.Models
{
    public class Blog
    {
        public ulong ID { get; set; }

        [Column(name: "authorid")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public required User.User Author { get; set; }

        [Column(name: "authorid")]
        public ulong AuthorID { get; set; }

        [MaxLength(60)]
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}
