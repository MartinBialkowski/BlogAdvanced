using BlogPost.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace BlogPost.Services.Tests
{
    public class StudentFixture
    {
        public DbContextOptions<BlogPostContext> Options { get; }
        public IEnumerable<StudentCourse> ExpectedMissingGrades { get; private set; }
        public StudentFixture()
        {
            Options = new DbContextOptionsBuilder<BlogPostContext>()
                .UseInMemoryDatabase(databaseName: "StudentContextOptions")
                .EnableSensitiveDataLogging()
                .Options;

            SeedDatabase(Options);
        }

        private void SeedDatabase(DbContextOptions<BlogPostContext> options)
        {
            using var context = new BlogPostContext(options);

            var assessments = new[]
            {
                new Assessment { WeightType = "Homework", Weight = 0.2f },
                new Assessment { WeightType = "Quiz test", Weight = 0.3f },
                new Assessment { WeightType = "Work at school", Weight = 0.6f },
                new Assessment { WeightType = "Exam", Weight = 1.0f }
            };

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

            context.Students.AddRange(students);
            context.Courses.AddRange(courses);
            context.Assessments.AddRange(assessments);
            context.StudentCourses.AddRange(
                new StudentCourse { Student = students[0], Course = courses[0], Assessment = assessments[0], Mark = 4 },
                new StudentCourse { Student = students[0], Course = courses[0], Assessment = assessments[1], Mark = 2 },
                new StudentCourse { Student = students[0], Course = courses[0], Assessment = assessments[2], Mark = 5 },
                new StudentCourse { Student = students[0], Course = courses[0], Assessment = assessments[3], Mark = 4 },
                new StudentCourse { Student = students[0], Course = courses[1], Assessment = assessments[3], Mark = 1 },
                new StudentCourse { Student = students[0], Course = courses[1], Assessment = assessments[0], Mark = 5 },
                new StudentCourse { Student = students[1], Course = courses[0], Assessment = assessments[0], Mark = 3 },
                new StudentCourse { Student = students[1], Course = courses[0], Assessment = assessments[1], Mark = 3 },
                new StudentCourse { Student = students[1], Course = courses[0], Assessment = assessments[2], Mark = 5 },
                new StudentCourse { Student = students[1], Course = courses[0], Assessment = assessments[3], Mark = 4 },
                new StudentCourse { Student = students[1], Course = courses[1], Assessment = assessments[1], Mark = 1 },
                new StudentCourse { Student = students[1], Course = courses[1], Assessment = assessments[2], Mark = 4 },
                new StudentCourse { Student = students[2], Course = courses[2], Assessment = assessments[1], Mark = 5 }
                );

            context.SaveChanges();

            ExpectedMissingGrades = GetMissingGrades(assessments, courses, students);
        }

        private IEnumerable<StudentCourse> GetMissingGrades(Assessment[] assessments, Course[] courses, Student[] students)
        {
            return new List<StudentCourse>
            {
                // Martin
                new StudentCourse(students[0], courses[1], assessments[1]),
                new StudentCourse(students[0], courses[1], assessments[2]),
                new StudentCourse(students[0], courses[2], assessments[0]),
                new StudentCourse(students[0], courses[2], assessments[1]),
                new StudentCourse(students[0], courses[2], assessments[2]),
                new StudentCourse(students[0], courses[2], assessments[3]),
                // Witalian
                new StudentCourse(students[1], courses[1], assessments[0]),
                new StudentCourse(students[1], courses[1], assessments[3]),
                new StudentCourse(students[1], courses[2], assessments[0]),
                new StudentCourse(students[1], courses[2], assessments[1]),
                new StudentCourse(students[1], courses[2], assessments[2]),
                new StudentCourse(students[1], courses[2], assessments[3]),
                // SomeRandom
                new StudentCourse(students[2], courses[0], assessments[0]),
                new StudentCourse(students[2], courses[0], assessments[1]),
                new StudentCourse(students[2], courses[0], assessments[2]),
                new StudentCourse(students[2], courses[0], assessments[3]),
                new StudentCourse(students[2], courses[1], assessments[0]),
                new StudentCourse(students[2], courses[1], assessments[1]),
                new StudentCourse(students[2], courses[1], assessments[2]),
                new StudentCourse(students[2], courses[1], assessments[3]),
                new StudentCourse(students[2], courses[2], assessments[0]),
                new StudentCourse(students[2], courses[2], assessments[2]),
                new StudentCourse(students[2], courses[2], assessments[3])
            };
        }
    }
}
