using BlogPost.Core.Entities;
using BlogPost.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Infrastructure
{
    public class StudentRepository : IStudentRepository
    {
        private readonly BlogPostContext context;

        public StudentRepository(BlogPostContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StudentCourse>> GetMissingGradesAsync()
        {
            var missingGrades = await context.StudentCourses
                .FromSqlRaw(
                    @"SELECT s.Id AS StudentId,
                             s.Name AS StudentName, 
                             c.Id AS CourseId,
                             c.Name AS CourseName,
                             a.Id AS AssessmentId, 
                             a.Weight, 
                             a.WeightType,
                             cast(0 AS float) AS Mark
                    FROM Students s
                    CROSS JOIN Courses c
                    CROSS JOIN Assessments a
                    LEFT JOIN studentCourses sc ON sc.CourseId = c.Id AND sc.StudentId = s.Id AND sc.AssessmentId = a.Id
                    WHERE sc.StudentId IS NULL")
                .Include(x => x.Student)
                .Include(x => x.Course)
                .Include(x => x.Assessment)
                .OrderBy(x => x.StudentId)
                .ThenBy(x => x.CourseId)
                .ToListAsync();

            return missingGrades;
        }
    }
}