using AutoMapper;

namespace BlogPost.WebApi.Types.Student
{
    public class StudentMapping: Profile
    {
        public StudentMapping()
        {
            CreateMap<Core.Entities.Student, StudentResponse>();
        }
    }
}
