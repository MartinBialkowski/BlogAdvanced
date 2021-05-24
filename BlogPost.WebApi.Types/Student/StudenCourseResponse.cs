namespace BlogPost.WebApi.Types.Student
{
    public class StudenCourseResponse
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int AssessmentId { get; set; }
        public string StudentName { get; set; } = "Unknown";
        public string CourseName { get; set; } = "Unknown";
        public string AssessmentName { get; set; } = "Unknown";
    }
}
