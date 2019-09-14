using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogPost.Core.Entities;
using AutoMapper;
using BlogPost.WebApi.Types.Student;
using BlogPost.WebApi.Types.Course;

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
        [ProducesResponseType(typeof(Student), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<ActionResult<StudentResponse>> GetStudent([FromRoute] int id)
        {
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
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 404)]
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
        [ProducesResponseType(typeof(Student), 201)]
        [ProducesResponseType(typeof(string), 400)]
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