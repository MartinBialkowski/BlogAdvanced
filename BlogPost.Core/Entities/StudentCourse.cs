using BlogPost.Core.Entities;

namespace BlogPost.Core
{
    public class StudentCourse
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int AssessmentId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Assessment Assessment { get; set; }
        public float Mark { get; set; }
    }
}
