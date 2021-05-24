using BlogPost.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPost.Core.Interfaces
{
    public interface IStudentService
    {
        Task<double> GetWeightedAverageForCourseAsync(int courseId, int studentId);
        Task<IEnumerable<StudentCourse>> GetMissingCoursesForStudentsAsync();
    }
}
