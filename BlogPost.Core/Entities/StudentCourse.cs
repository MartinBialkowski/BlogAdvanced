namespace BlogPost.Core.Entities
{
    public class StudentCourse
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int AssessmentId { get; set; }
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Assessment Assessment { get; set; } = null!;
        public double Mark { get; set; }

        public StudentCourse()
        {
        }

        public StudentCourse(Student student, Course course, Assessment assessment)
        {
            Student = student;
            StudentId = student.Id;
            Course = course;
            CourseId = course.Id;
            Assessment = assessment;
            AssessmentId = assessment.Id;
            Mark = 0;
        }
    }
}
