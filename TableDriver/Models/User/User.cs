namespace TableDriver.Models.User
{
    /// <summary>
    /// This is for the table.
    /// </summary>
    public class User : UserBase
    {
        public List<Blog.Blog> Blogs { get; set; } = new List<Blog.Blog>();
    }
}
