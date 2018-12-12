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
            if (!context.Courses.Any() && !context.Students.Any())
            {
                var students = new[]
                {
                    new Student { Name = "Martin B" },
                    new Student { Name = "Witalian" },
                    new Student { Name =" SomeRandom" }
                };

                var courses = new[]
                {
                    new Course { Name = "Informatic" },
                    new Course { Name = "Language" },
                    new Course { Name = "Math" }
                };

                context.StudentCourses.AddRange(
                    new StudentCourse { Student = students[0], Course = courses[0], AssessmentId = 1 },
                    new StudentCourse { Student = students[0], Course = courses[0], AssessmentId = 4 },
                    new StudentCourse { Student = students[0], Course = courses[1], AssessmentId = 1 },
                    new StudentCourse { Student = students[1], Course = courses[1], AssessmentId = 2 },
                    new StudentCourse { Student = students[1], Course = courses[0], AssessmentId = 3 },
                    new StudentCourse { Student = students[2], Course = courses[2], AssessmentId = 2 }
                    );

                context.SaveChanges();
            }
        }
    }
}

