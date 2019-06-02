using AutoMapper;

namespace BlogPost.WebApi.Types.Course
{
    public class CourseMapping: Profile
    {
        public CourseMapping()
        {
            CreateMap<Core.Entities.StudentCourse, CourseResponse>()
                .ForMember(dest => dest.Name, m => m.MapFrom(src => src.Course.Name));
        }
    }
}
