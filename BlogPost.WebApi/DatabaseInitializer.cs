using BlogPost.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BlogPost.WebApi
{
    public class DatabaseInitializer
    {
        private static BlogPostContext context;

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                context = serviceScope.ServiceProvider.GetRequiredService<BlogPostContext>();
                context.Database.Migrate();
                InitilizeDatabase();
            }
        }

        private static void InitilizeDatabase()
        {
            if (!context.Courses.Any())
            {
                context.Courses.Add(new Course
                {
                    Name = "Random"
                });

                context.Courses.Add(new Course
                {
                    Name = "New Course"
                });

                context.SaveChanges();
            }

            if (!context.Students.Any())
            {
                context.Students.Add(new Student
                {
                    Name = "Martin B"
                });

                context.Students.Add(new Student
                {
                    Name = "Witalian"
                });

                context.Students.Add(new Student
                {
                    Name = "SomeRandomStudent"
                });

                context.SaveChanges();
            }
        }
    }
}

