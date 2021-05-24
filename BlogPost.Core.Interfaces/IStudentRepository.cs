using BlogPost.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Core.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentCourse>> GetMissingGradesAsync();
    }
}
