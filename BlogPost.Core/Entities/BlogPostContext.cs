using Microsoft.EntityFrameworkCore;

namespace BlogPost.Core.Entities
{
    public class BlogPostContext : DbContext
    {
        public BlogPostContext(DbContextOptions<BlogPostContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(x => new { x.CourseId, x.StudentId, x.AssessmentId });

            modelBuilder.Entity<Assessment>()
                .HasData(
                new Assessment { Id = 1, WeightType = "Homework", Weight = 0.2f },
                new Assessment { Id = 2, WeightType = "Quiz test", Weight = 0.3f },
                new Assessment { Id = 3, WeightType = "Work at school", Weight = 0.6f },
                new Assessment { Id = 4, WeightType = "Exam", Weight = 1.0f });
        }
    }
}
