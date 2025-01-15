namespace TableDriver.Models.User
{
    public class User : UserBase
    {
        public List<Blog.Blog>? Blogs { get; set; }
    }
}
