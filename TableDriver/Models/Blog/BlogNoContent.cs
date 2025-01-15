using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TableDriver.Models.Blog
{
    public class BlogNoContent
    {
        public ulong ID { get; set; }

        [Column(name: "authorid")]
        [InverseProperty("Blogs")]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User.User? Author { get; set; }

        [Column(name: "authorid")]
        public ulong AuthorID { get; set; }

        [MaxLength(60)]
        public string Title { get; set; } = string.Empty;
    }
}
