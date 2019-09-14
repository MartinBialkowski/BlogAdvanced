using BlogPost.Core.Entities;
using BlogPost.WebApi.Types.Student;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlogPost.WebApi.Controllers.Tests
{
    public class StudentControllerTest: IClassFixture<ControllerFixture>
    {
        private readonly ControllerFixture fixture;

        public StudentControllerTest(ControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Should_ReturnSingleStudent_WithCourses()
        {
            // arrange
            const int firstElement = 0;
            ActionResult<StudentResponse> result;
            var expected = fixture.ExpectedStudents.ElementAt(firstElement);

            // act
            using (var dbContext = new BlogPostContext(fixture.Options))
            {
                var controller = new StudentsController(dbContext, fixture.Mapper);
                result = await controller.GetStudent(ControllerFixture.StudentId);
            }

            var okResult = result.Result.As<OkObjectResult>();
            var students = okResult.Value.As<StudentResponse>();

            // assert
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            students.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task Should_AddNewStudent()
        {
            // arrange
            ActionResult<StudentResponse> result;
            var studentRequest = new CreateStudentRequest { Name = fixture.NewStudent.Name };
            Student studentResult;

            // act
            using (var dbContext = new BlogPostContext(fixture.Options))
            {
                var controller = new StudentsController(dbContext, fixture.Mapper);
                result = await controller.PostStudent(studentRequest);
                studentResult = dbContext.Students.FirstOrDefault(x => x.Name == fixture.NewStudent.Name);
            }

            var apiResult = result.Result.As<CreatedAtActionResult>();
            var students = apiResult.Value.As<StudentResponse>();

            // assert
            apiResult.StatusCode.Should().Be(StatusCodes.Status201Created);
            students.Should().BeEquivalentTo(studentResult);
        }

        [Fact]
        public async Task Should_UpdateExistingStudent()
        {
            // arrange
            IActionResult result;
            var studentRequest = new UpdateStudentRequest { Name = fixture.UpdatedStudent.Name, Id = ControllerFixture.StudentId };
            Student studentResult;

            // act
            using (var dbContext = new BlogPostContext(fixture.Options))
            {
                var controller = new StudentsController(dbContext, fixture.Mapper);
                result = await controller.PutStudent(ControllerFixture.StudentId, studentRequest);
                studentResult = dbContext.Students.First(x => x.Id == ControllerFixture.StudentId);
            }

            //assert
            var noContentResult = result.As<NoContentResult>();
            noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            studentResult.Name.Should().Be(studentRequest.Name);
        }

        [Fact]
        public async Task Should_RemoveExistingStudent()
        {
            // arrange
            const int studentId = 2;
            IActionResult result;
            Student studentResult;

            // act
            using (var dbContext = new BlogPostContext(fixture.Options))
            {
                var controller = new StudentsController(dbContext, fixture.Mapper);
                result = await controller.DeleteStudent(studentId);
                studentResult = dbContext.Students.FirstOrDefault(x => x.Id == studentId);
            }

            // assert
            var noContentResult = result.As<NoContentResult>();
            noContentResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            studentResult.Should().BeNull();
        }
    }
}
