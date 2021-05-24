using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogPost.Core.Entities;
using AutoMapper;
using BlogPost.WebApi.Types.Student;
using BlogPost.WebApi.Types.Course;
using BlogPost.Core.Interfaces;
using System;
using Microsoft.AspNetCore.Http;

namespace BlogPost.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly BlogPostContext context;
        private readonly IMapper mapper;
        private readonly IStudentService studentService;
        private readonly IStudentRepository studentRepository;

        public StudentsController(BlogPostContext context, IMapper mapper, IStudentService studentService, IStudentRepository studentRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.studentService = studentService;
            this.studentRepository = studentRepository;
        }

        // GET: api/Students
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Student>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<StudentResponse>>> GetStudents()
        {
            var students = await context.Students
                .Include(x => x.Courses)
                .ThenInclude(sc => sc.Course)
                .ToListAsync();
            var response = mapper.Map<IEnumerable<StudentResponse>>(students);

            return Ok(response);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentResponse>> GetStudent([FromRoute] int id)
        {
            await studentService.GetMissingCoursesForStudentsAsync();

            var student = await context.Students
                .Include(x => x.Courses)
                .ThenInclude(sc => sc.Course)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var response = mapper.Map<StudentResponse>(student);

            return Ok(response);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] UpdateStudentRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var student = mapper.Map<Student>(request);

            context.Entry(student).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        [ProducesResponseType(typeof(Student), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StudentResponse>> PostStudent([FromBody] CreateStudentRequest request)
        {
            var student = mapper.Map<Student>(request);
            context.Students.Add(student);
            await context.SaveChangesAsync();
            var studentResponse = mapper.Map<StudentResponse>(student);

            return CreatedAtAction("GetStudent", new { id = studentResponse.Id }, studentResponse);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            context.Students.Remove(student);
            await context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/students/5/courses/5/weighted-average
        [HttpGet("{studentId}/courses/{courseId}/weighted-average")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<double>> GetWeightedAverage(int studentId, int courseId)
        {
            try
            {
                var result = await studentService.GetWeightedAverageForCourseAsync(courseId, studentId);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/students/missing-grades-linq
        [HttpGet("missing-grades-linq")]
        public async Task<ActionResult<IEnumerable<StudenCourseResponse>>> GetMissingGrades()
        {
            var missingGrades = await studentService.GetMissingCoursesForStudentsAsync();
            var result = mapper.Map<IEnumerable<StudenCourseResponse>>(missingGrades);
            return Ok(result);
        }

        // GET: api/students/missing-grades-linq
        [HttpGet("missing-grades-sql")]
        public async Task<ActionResult<IEnumerable<StudenCourseResponse>>> GetMissingGradesSQL()
        {
            var missingGrades = await studentRepository.GetMissingGradesAsync();
            var result = mapper.Map<IEnumerable<StudenCourseResponse>>(missingGrades);
            return Ok(result);
        }

        private bool StudentExists(int id)
        {
            return context.Students.Any(e => e.Id == id);
        }
    }
}