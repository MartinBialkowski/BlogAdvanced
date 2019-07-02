using FluentValidation;

namespace BlogPost.WebApi.Types.Student
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(student => student.Name)
                .NotEmpty().WithMessage("Student name is required.");
        }
    }
}
