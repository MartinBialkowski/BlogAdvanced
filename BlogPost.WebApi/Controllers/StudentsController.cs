using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogPost.Core.Entities;
using AutoMapper;
using BlogPost.WebApi.Types.Student;

namespace BlogPost.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly BlogPostContext context;
        private readonly IMapper mapper;

        public StudentsController(BlogPostContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/Students
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Student>), 200)]
        public ActionResult<IEnumerable<StudentResponse>> GetStudents()
        {
            var students = context.Students
                .Include(x => x.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .ToList();
            var response = mapper.Map<IEnumerable<Student>, IEnumerable<StudentResponse>>(students);

            return Ok(response);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Student), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<ActionResult<StudentResponse>> GetStudent([FromRoute] int id)
        {
            var student = await context.Students
                .Include(x => x.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

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
        [ProducesResponseType(typeof(Student), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult<Student>> PostStudent([FromBody] Student student)
        {
            context.Students.Add(student);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(void), 404)]
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

        private bool StudentExists(int id)
        {
            return context.Students.Any(e => e.Id == id);
        }
    }
}