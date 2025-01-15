using Microsoft.EntityFrameworkCore;
using TableDriver.DBContexts;
using TableDriver.Models.Blog;

namespace TableDriver.Services;
public class BlogService(UserContext userContext)
{
    public List<BlogNoContent> GetBlogsTitleByAuthor(string author)
    {
        if (ulong.TryParse(author, out ulong authorulong))
        {
            List<BlogNoContent> result = userContext.Blog.Where(row => row.AuthorID == authorulong).Select(row => new BlogNoContent() { ID = row.ID, Title = row.Title, AuthorID = row.AuthorID, Author = row.Author }).ToList();
            return result;
        }
        {
            List<BlogNoContent> result = userContext.Blog.Include(row => row.Author).Where(row => row.Author.Username == author).Select(row => new BlogNoContent() { ID = row.ID, Title = row.Title, AuthorID = row.AuthorID, Author = row.Author }).ToList();
            return result;
        }
    }
}
