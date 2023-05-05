using System.Reflection;
using Clevyr.Blogs.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Clevyr.Blogs.Api.Data;

public class DataContext : DbContext
{
    private readonly IConfiguration Configuration;

    public DataContext()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(
            Configuration.GetConnectionString("WebApiDatabase"),
            options => { options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName); }
        );
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }


    public static Task Migrate() => new DataContext().Database.MigrateAsync();

    public static Task Seed()
    {
        using var context = new DataContext();
        if (context.Blogs.Any())
            return Task.CompletedTask;

        var blogs = new List<Blog>
        {
            new()
            {
                Name = "Awesome Blog",
                Author = "Chris",
                Synopsis = "An awesome blog of awesome awesomeness.",
                Posts = new List<Post>
                {
                    new() { Content = "Awesome Stuff" },
                    new() { Content = "Other Awesome Stuff" },
                    new() { Content = "More Awesome Stuff" },
                },
            },
            new()
            {
                Name = "Okayish Blog",
                Author = "Aaron",
                Synopsis = "A meh blog of meh mehness.",
                Posts = new List<Post>
                {
                    new() { Content = "Okayish Stuff" },
                    new() { Content = "Other Okayish Stuff" },
                    new() { Content = "More Okayish Stuff" },
                },
            },
            new()
            {
                Name = "Freakin' Sweet Blog",
                Author = "Tim",
                Synopsis = "A freakin' sweet blog of freakin' sweet freakin' sweetness.",
                Posts = new List<Post>
                {
                    new() { Content = "Freakin' Sweet Stuff" },
                    new() { Content = "Other Freakin' Sweet Stuff" },
                    new() { Content = "More Freakin' Sweet Stuff" },
                },
            },
        };

        context.Database.EnsureCreated();
        context.Blogs.AddRange(blogs);
        return context.SaveChangesAsync();
    }
}