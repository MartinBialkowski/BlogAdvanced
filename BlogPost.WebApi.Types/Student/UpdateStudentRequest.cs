namespace BlogPost.WebApi.Types.Student
{
    public class UpdateStudentRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
