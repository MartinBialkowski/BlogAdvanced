using AutoMapper;

namespace BlogPost.WebApi.Types.Course
{
    public class CourseMapping: Profile
    {
        public CourseMapping()
        {
            CreateMap<Core.Entities.StudentCourse, CourseResponse>(MemberList.Destination)
                .ForMember(dest => dest.Id, m => m.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.Course.Name));
        }
    }
}
