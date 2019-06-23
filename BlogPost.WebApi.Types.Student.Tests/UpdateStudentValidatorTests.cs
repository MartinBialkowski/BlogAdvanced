using FluentValidation.TestHelper;
using Xunit;

namespace BlogPost.WebApi.Types.Student.Tests
{
    public class UpdateStudentValidatorTests
    {
        private readonly UpdateStudentValidator validator;

        public UpdateStudentValidatorTests()
        {
            validator = new UpdateStudentValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData("")]
        public void InvalidWhenNameIsNotValid(string name)
        {
            validator.ShouldHaveValidationErrorFor(x => x.Name, name);
        }

        [Fact]
        public void ValidWhenNameProvided()
        {
            const string validName = "Name";
            validator.ShouldNotHaveValidationErrorFor(x => x.Name, validName);
        }
    }
}
