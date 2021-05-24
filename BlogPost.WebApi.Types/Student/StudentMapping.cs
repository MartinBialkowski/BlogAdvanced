using AutoMapper;

namespace BlogPost.WebApi.Types.Student
{
    public class StudentMapping: Profile
    {
        public StudentMapping()
        {
            CreateMap<Core.Entities.Student, StudentResponse>(MemberList.Destination);
            CreateMap<UpdateStudentRequest, Core.Entities.Student>(MemberList.Source);
            CreateMap<CreateStudentRequest, Core.Entities.Student>(MemberList.Source);
            CreateMap<Core.Entities.StudentCourse, StudenCourseResponse>(MemberList.Destination)
                .ForMember(dest => dest.AssessmentId, src => src.MapFrom(x => x.AssessmentId))
                .ForMember(dest => dest.AssessmentName, src => src.MapFrom(x => x.Assessment.WeightType))
                .ForMember(dest => dest.CourseId, src => src.MapFrom(x => x.CourseId))
                .ForMember(dest => dest.CourseName, src => src.MapFrom(x => x.Course.Name))
                .ForMember(dest => dest.StudentId, src => src.MapFrom(x => x.StudentId))
                .ForMember(dest => dest.StudentName, src => src.MapFrom(x => x.Student.Name));

        }
    }
}
