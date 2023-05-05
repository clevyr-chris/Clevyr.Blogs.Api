namespace Clevyr.Blogs.Api.Models;

public class Blog
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; }
    public virtual string Author { get; set; }
    public virtual string Synopsis { get; set; }

    public virtual List<Post> Posts { get; set; }

}