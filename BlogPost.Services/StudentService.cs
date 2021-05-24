using BlogPost.Core.Entities;
using BlogPost.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPost.Services
{
    public class StudentService : IStudentService
    {
        private readonly BlogPostContext context;

        public StudentService(BlogPostContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StudentCourse>> GetMissingCoursesForStudentsAsync()
        {
            var crossJoin = await (from student in context.Students
                                   from course in context.Courses
                                   from assessment in context.Assessments
                                   select new StudentCourse(student, course, assessment))
                            .ToListAsync();
            // version 1
            var q = from all in crossJoin
                    join existing in context.StudentCourses on new { all.StudentId, all.CourseId, all.AssessmentId }
                    equals new { existing.StudentId, existing.CourseId, existing.AssessmentId } into gj
                    from a in gj.DefaultIfEmpty()
                    where a == null
                    orderby all.StudentId, all.CourseId, all.AssessmentId
                    select all;
            
            // version 2
            return crossJoin.GroupJoin(context.StudentCourses,
                    all => new { all.StudentId, all.CourseId, all.AssessmentId },
                    existing => new { existing.StudentId, existing.CourseId, existing.AssessmentId },
                    (all, existings) => new { all, existings })
                .SelectMany(result => result.existings.DefaultIfEmpty(),
                    (result, existing) => new { result.all, existing })
                .Where(x => x.existing == null)
                .Select(x => x.all)
                .OrderBy(x => x.StudentId)
                .ThenBy(x => x.CourseId)
                .ThenBy(x => x.AssessmentId)
                .ToList();
        }

        public async Task<double> GetWeightedAverageForCourseAsync(int courseId, int studentId)
        {
            var courses = await context.StudentCourses
                .Include(x => x.Assessment)
                .Where(x => x.StudentId == studentId && x.CourseId == courseId)
                .ToListAsync();

            if(!courses.Any())
            {
                throw new ArgumentException($"Student with Id {studentId} or Course with Id {courseId} not exist, or student isn't assigned to course");
            }

            return CalculateGradesWeighterAverage(courses);           
        }

        private double CalculateGradesWeighterAverage(IEnumerable<StudentCourse> courses)
        {
            double sum = 0;
            double weights = 0;

            foreach (var course in courses)
            {
                sum += course.Mark * course.Assessment.Weight;
                weights += course.Assessment.Weight;
            }

            return sum / weights;
        }
    }
}
