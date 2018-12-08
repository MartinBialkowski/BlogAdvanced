using Microsoft.EntityFrameworkCore;

namespace BlogPost.Core.Entities
{
    public class BlogPostContext: DbContext
    {
        public BlogPostContext(DbContextOptions<BlogPostContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
