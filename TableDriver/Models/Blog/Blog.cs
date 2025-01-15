namespace TableDriver.Models.Blog
{
    public class Blog : BlogNoContent
    {
        public string Content { get; set; } = string.Empty;

        public BlogNoContent CloneToBlogNoContent
        {
            get
            {
                return new BlogNoContent()
                {
                    ID = ID,
                    AuthorID = AuthorID,
                    Title = Title
                };
            }
        }
    }
}
