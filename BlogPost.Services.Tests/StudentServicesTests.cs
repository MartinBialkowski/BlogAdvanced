using BlogPost.Core.Entities;
using FluentAssertions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlogPost.Services.Tests
{
    public class StudentServicesTests : IClassFixture<StudentFixture>
    {
        private readonly StudentFixture fixture;
        private StudentService sut;

        public StudentServicesTests(StudentFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Should_ThrowException_When_StudentNotExist()
        {
            // arrange
            const int notExistingStudentId = 666;
            const int courseId = 1;
            using var dbContext = new BlogPostContext(fixture.Options);
            sut = new StudentService(dbContext);

            // act
            Func<Task> act = async () => await sut.GetWeightedAverageForCourseAsync(courseId, notExistingStudentId);

            // assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Should_ThrowException_When_CourseNotExist()
        {
            // arrange
            const int studentId = 1;
            const int notExistingCourseId = 666;
            using var dbContext = new BlogPostContext(fixture.Options);
            sut = new StudentService(dbContext);

            // act
            Func<Task> act = async () => await sut.GetWeightedAverageForCourseAsync(notExistingCourseId, studentId);

            // assert
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public async Task Should_CalculateWeighterAverage()
        {
            // arrange
            var expected = GetExpectedWeighterAverage();
            using var dbContext = new BlogPostContext(fixture.Options);
            sut = new StudentService(dbContext);

            // act
            var result = await sut.GetWeightedAverageForCourseAsync(courseId: 1, studentId: 1);

            // assert
            result.Should().Be(expected);
        }

        [Fact]
        public async Task Should_ReturnMissingGrades_ForEveryStudent()
        {
            // arrange
            using var dbContext = new BlogPostContext(fixture.Options);
            sut = new StudentService(dbContext);

            // act
            var result = await sut.GetMissingCoursesForStudentsAsync();

            // assert
            result.Should().HaveCount(fixture.ExpectedMissingGrades.Count());
            result.Should().BeEquivalentTo(fixture.ExpectedMissingGrades, 
                options => options
                    .IgnoringCyclicReferences()
                    .Excluding(x => x.Assessment)
                    .Excluding(x => x.Course)
                    .Excluding(x => x.Student)
                );
        }

        private double GetExpectedWeighterAverage()
        {
            return ((4 * 0.2) + (2 * 0.3) + (5 * 0.6) + (4 * 1)) / 2.1;
        }
    }
}
