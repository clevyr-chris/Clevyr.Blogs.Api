using Clevyr.Blogs.Api.Data;
using Clevyr.Blogs.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clevyr.Blogs.Api.Controllers;

[ApiController, Route("[controller]")]
public class BlogsController
{
    private readonly ILogger<BlogsController> _logger;


    public BlogsController(ILogger<BlogsController> logger) => _logger = logger;


    [HttpGet]
    public Task<List<Blog>> Get() => new DataContext().Blogs.Include(v => v.Posts).ToListAsync();

    [HttpPost]
    public async Task Post(Blog blog)
    {
        await using var context = new DataContext();

        if (blog.Id > 0)
            context.Update(blog);
        else
            context.Add(blog);

        await context.SaveChangesAsync();
    }
}