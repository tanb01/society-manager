using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyManager.Data.Entities;
using SocietyManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SocietyManager.Controllers
{
    public class StudentsController : ApiControllerBase
    {
        // GET: api/<StudentsController>
        [HttpGet(Name = nameof(GetStudents))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetStudents()
        {
            var students = await Context.Students
                .AsNoTracking()
                .ProjectTo<GetStudentDto>(Mapper.ConfigurationProvider)
                .ToArrayAsync();

            return Ok(students);
        }

        // GET: api/<StudentsController>/5
        [HttpGet("{id}", Name = nameof(GetStudent))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = await Context.Students
                .AsNoTracking()
                .ProjectTo<GetStudentDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(e => e.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/<StudentsController>/5
        [HttpPut("{id}", Name = nameof(PutStudent))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetStudentDto>> PutStudent([FromRoute] int id, [FromBody] UpdateStudentDto newStudentModel)
        {
            try
            {
                var oldStudent = await Context.Students.FindAsync(id);
                if (oldStudent == null)
                {
                    return NotFound($"Society with id of {id} could not be found");
                }

                var updatedStudent = Mapper.Map(newStudentModel, oldStudent);

                await Context.SaveChangesAsync();
                return Mapper.Map<GetStudentDto>(updatedStudent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound($"Society with id of {id} was not found");
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to update a society");
                }
            }
        }

        // POST: api/<StudentsController>
        [HttpPost(Name = nameof(PostStudent))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<CreateStudentDto>> PostStudent([FromBody] CreateStudentDto student)
        {
            var aStudent = await Context.Students.SingleOrDefaultAsync(st => st.Email == student.Email);

            if (aStudent != null)
            {
                return BadRequest("A student with the same email already exists");
            }

            var stu = Mapper.Map<Student>(student);

            Context.Students.Add(stu);
            await Context.SaveChangesAsync();
            var newStudent = Mapper.Map<GetStudentDto>(stu);

            return CreatedAtAction(nameof(GetStudent), new { id = newStudent.StudentId }, newStudent);
        }

        // DELETE: api/<StudentsController>/5
        [HttpDelete("{id}", Name = nameof(DeleteStudent))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            var student = await Context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound($"Student with id of {id} was not found");
            }

            Context.Students.Remove(student);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return Context.Students.Any(e => e.StudentId == id);
        }
    }
}
