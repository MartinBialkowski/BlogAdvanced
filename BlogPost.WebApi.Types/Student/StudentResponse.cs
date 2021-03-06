﻿using BlogPost.WebApi.Types.Course;
using System.Collections.Generic;

namespace BlogPost.WebApi.Types.Student
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<CourseResponse> Courses { get; set; } = new HashSet<CourseResponse>();
    }
}
