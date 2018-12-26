using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogPost.Core.Entities
{
    public class Assessment
    {
        public int Id { get; set; }
        [Required]
        public float Weight { get; set; }
        [Required, StringLength(50)]
        public string WeightType { get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
    }
}
