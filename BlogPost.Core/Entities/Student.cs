using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogPost.Core.Entities
{
    public class Student : IEntityBase
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; } = null!;
        public ICollection<StudentCourse> Courses { get; set; } = new HashSet<StudentCourse>();
    }
}
