using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogPost.Core.Entities
{
    public class Assessment
    {
        public int Id { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required, StringLength(50)]
        public string WeightType { get; set; } = null!;
        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
    }
}
