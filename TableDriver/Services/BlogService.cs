using Microsoft.EntityFrameworkCore;
using TableDriver.DBContexts;
using TableDriver.Models.Blog;

namespace TableDriver.Services;
public class BlogService(UserContext userContext)
{
    public List<BlogNoContent> GetBlogsTitleByAuthor(string author)
    {
        if (long.TryParse(author, out long authorulong))
        {
            List<BlogNoContent> result = (from blog in userContext.Blog.AsNoTracking()
                                          where blog.AuthorID == authorulong
                                          select new BlogNoContent() { ID = blog.ID, AuthorID = blog.AuthorID, Title = blog.Title })
                                          .ToList();

            // doesn't work anymore in postgres, select introduce joining User for no reason
            // List<BlogNoContent> result = userContext.Blog.AsNoTracking()
            // .Where(row => row.AuthorID == authorulong)
            // .Select(row => new BlogNoContent() { ID = row.ID, Title = row.Title, AuthorID = row.AuthorID, Author = row.Author })
            // .ToList();
            return result;
        }
        {
            var result2 = (from uuu in userContext.User.AsNoTracking() where uuu.Username == author join blog in userContext.Blog.AsNoTracking() on uuu.ID equals blog.AuthorID select new BlogNoContent() { ID = blog.ID, AuthorID = blog.AuthorID, Title = blog.Title }).ToList();
            // LINQ use the new statement as select, not function or property
            if (result2 is null)
            {
                return new List<BlogNoContent>();
            }
            return result2;
        }
    }
}
