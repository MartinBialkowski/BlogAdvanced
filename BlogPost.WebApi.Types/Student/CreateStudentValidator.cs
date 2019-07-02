using FluentValidation;

namespace BlogPost.WebApi.Types.Student
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(student => student.Name)
                .NotEmpty().WithMessage("Student name is required.");
        }
    }
}
