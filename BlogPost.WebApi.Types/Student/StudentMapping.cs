using AutoMapper;

namespace BlogPost.WebApi.Types.Student
{
    public class StudentMapping: Profile
    {
        public StudentMapping()
        {
            CreateMap<Core.Entities.Student, StudentResponse>(MemberList.Source);
            CreateMap<UpdateStudentRequest, Core.Entities.Student>(MemberList.Source);
            CreateMap<CreateStudentRequest, Core.Entities.Student>(MemberList.Source);
        }
    }
}
