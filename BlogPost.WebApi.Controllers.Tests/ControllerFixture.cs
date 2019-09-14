using AutoMapper;
using BlogPost.Core.Entities;
using BlogPost.WebApi.Types.Course;
using BlogPost.WebApi.Types.Student;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlogPost.WebApi.Controllers.Tests
{
    public class ControllerFixture
    {
        public DbContextOptions<BlogPostContext> Options { get; private set; }
        public IEnumerable<StudentResponse> ExpectedStudents { get; private set; }
        public IMapper Mapper { get; private set; }
        private IEnumerable<Student> allStudents;
        public readonly Student NewStudent;
        public readonly Student UpdatedStudent;
        public const int StudentId = 1;

        public ControllerFixture()
        {
            SetupAutoMapper();
            SetupDatabase();

            NewStudent = new Student { Name = "Martin" };
            UpdatedStudent = new Student { Name = "Wit", Id = StudentId };
        }

        private void SetupAutoMapper()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.CreateMap<Student, StudentResponse>();
                opts.CreateMap<CreateStudentRequest, Student>();
                opts.CreateMap<UpdateStudentRequest, Student>();
                opts.CreateMap<StudentCourse, CourseResponse>()
                    .ForMember(dest => dest.Id, m => m.MapFrom(src => src.CourseId))
                    .ForMember(dest => dest.Name, m => m.MapFrom(src => src.Course.Name));
            });

            Mapper = config.CreateMapper();
        }

        private void SetupDatabase()
        {
            Options = new DbContextOptionsBuilder<BlogPostContext>()
               .UseInMemoryDatabase(databaseName: "StudentContextOptions")
               .Options;

            using (var dbContext = new BlogPostContext(Options))
            {
                var itCourse = new Course
                {
                    Name = "IT Programming"
                };

                var math = new Course
                {
                    Name = "Math"
                };

                var student1 = new Student
                {
                    Name = "John Doe",
                    Courses = new List<StudentCourse>
                    {
                        new StudentCourse { Course = itCourse },
                        new StudentCourse { Course = math }
                    }
                };

                var student2 = new Student
                {
                    Name = "Martin B",
                    Courses = new List<StudentCourse>
                    {
                        new StudentCourse { Course = itCourse },
                        new StudentCourse { Course = math }
                    }
                };

                dbContext.Add(student1);
                dbContext.Add(student2);

                dbContext.SaveChanges();

                allStudents = new List<Student> { student1, student2 };
                ExpectedStudents = Mapper.Map<IEnumerable<StudentResponse>>(allStudents);
            }
        }
    }
}
