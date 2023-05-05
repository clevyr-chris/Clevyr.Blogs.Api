namespace Clevyr.Blogs.Api.Models;

public class Post
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
}