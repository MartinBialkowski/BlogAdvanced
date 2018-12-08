using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogPost.Core.Entities;

namespace BlogPost.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly BlogPostContext context;

        public StudentsController(BlogPostContext context)
        {
            this.context = context;
        }

        // GET: api/Students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return context.Students;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent([FromRoute] int id)
        {
            var student = await context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
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
        public async Task<ActionResult<Student>> PostStudent([FromBody] Student student)
        {
            context.Students.Add(student);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
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