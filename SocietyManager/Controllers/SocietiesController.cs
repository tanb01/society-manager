using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyManager.Data.Entities;
using SocietyManager.Models;
using SocietyManager.Models.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocietyManager.Controllers
{
    public class SocietiesController : ApiControllerBase
    {

        // GET: api/<SocietiesController>
        [HttpGet(Name = nameof(GetSocieties))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetSocieties([FromQuery] int limit = 3, [FromQuery] int page = 1)
        {
            int skip = (page - 1) * limit;

            var societies = await Context.Societies
                .AsNoTracking()
                .ProjectTo<GetSocietyDto>(Mapper.ConfigurationProvider)
                .OrderBy(s => s.Name)
                .Skip(skip)
                .Take(limit)
                .ToArrayAsync();

            return Ok(societies);
        }

        // GET: api/<SocietiesController>/5
        [HttpGet("{id}", Name = nameof(GetSociety))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetSociety([FromRoute] int id)
        {
            var society = await Context.Societies
                .AsNoTracking()
                .ProjectTo<GetSocietyDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(s => s.SocietyId == id);

            if (society == null)
            {
                return NotFound();
            }

            return Ok(society);
        }

        // PUT: api/<SocietiesController>/5
        [HttpPut("{id}", Name = nameof(PutSociety))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetSocietyDto>> PutSociety([FromRoute] int id, [FromBody] UpdateSocietyDto newSocietyModel)
        {
            try
            {
                var oldSociety = await Context.Societies.FindAsync(id);
                if (oldSociety == null)
                {
                    return NotFound($"Society with id of {id} could not be found");
                }

                var updatedSociety = Mapper.Map(newSocietyModel, oldSociety);

                await Context.SaveChangesAsync();
                return Mapper.Map<GetSocietyDto>(updatedSociety);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocietyExists(id))
                {
                    return NotFound($"Society with id of {id} was not found");
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to update a society");
                }
            }
        }

        // POST: api/<SocietiesController>
        [HttpPost(Name = nameof(PostSociety))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetSocietyDto>> PostSociety([FromBody] CreateSocietyDto society)
        {
            var aSociety = await Context.Societies.SingleOrDefaultAsync(s => s.Name == society.Name);

            if (aSociety != null)
            {
                return BadRequest($"A society with name {society.Name} already exists");
            }

            var soc = Mapper.Map<Society>(society);
            soc.CreationDate = DateTime.UtcNow;

            Context.Societies.Add(soc);
            await Context.SaveChangesAsync();
            var newSociety = Mapper.Map<GetSocietyDto>(soc);

            return CreatedAtAction(nameof(GetSociety), new { id = newSociety.SocietyId }, newSociety);
        }

        // DELETE: api/<SocietiesController>/5
        [HttpDelete("{id}", Name = nameof(DeleteSociety))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteSociety([FromRoute] int id)
        {
            var society = await Context.Societies.FindAsync(id);
            if (society == null)
            {
                return NotFound($"Society with id of {id} was not found");
            }

            Context.Societies.Remove(society);
            await Context.SaveChangesAsync();
            return NoContent();
        }

        // POST
        [HttpPost("{societyId:int}/students/{studentId:int}", Name = nameof(AddMember))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddMember([FromRoute] int societyId, [FromRoute] int studentId)
        {
            var society = await Context.Societies
                .Include(x => x.Members)
                .SingleOrDefaultAsync(x => x.SocietyId == societyId);

            var student = await Context.Students
                .AsNoTracking()
                .SingleOrDefaultAsync(st => st.StudentId == studentId);

            if (society == null || student == null)
            {
                return NotFound();
            }

            if (society.Members.Any(m => m.StudentId == studentId))
            {
                return BadRequest("This member already exists");
            }

            society.Members.Add(new SocietyStudent { Society = society, Student = student });

            await Context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{societyId:int}/students/{studentId:int}", Name = nameof(RemoveMember))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveMember([FromRoute] int societyId, [FromRoute] int studentId)
        {
            var society = await Context.Societies
                .Include(s => s.Members)
                .SingleOrDefaultAsync(s => s.SocietyId == societyId);

            var student = society?.Members.SingleOrDefault(m => m.StudentId == studentId);

            if (society == null || student == null)
            {
                return NotFound();
            }

            society.Members.Remove(student);

            await Context.SaveChangesAsync();
            return NoContent();
        }

        // GET
        [HttpGet("{societyId:int}/students", Name = nameof(GetMembers))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetStudentDto[]>> GetMembers([FromRoute] int societyId)
        {
            var members = await Context.Societies
                .AsNoTracking()
                .Include(s => s.Members)
                .ThenInclude(s => s.Student)
                .Where(s => s.SocietyId == societyId)
                .SelectMany(s => s.Members.Select(x => x.Student))
                .ProjectTo<GetStudentDto>(Mapper.ConfigurationProvider)
                .ToArrayAsync();

            return Ok(members);
        }

        // POST
        [HttpPost("{societyId:int}/events/{eventId:int}", Name = nameof(AddEvent))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddEvent([FromRoute] int societyId, [FromRoute] int eventId)
        {
            var society = await Context.Societies
                .Include(s => s.Events)
                .SingleOrDefaultAsync(s => s.SocietyId == societyId);

            var anEvent = await Context.Events
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.EventId == eventId);

            if (society == null || anEvent == null)
            {
                return NotFound();
            }

            if (society.Events.Any(e => e.EventId == eventId))
            {
                return BadRequest("This event already exists");
            }

            society.Events.Add(new SocietyEvent { Society = society, Event = anEvent });

            await Context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE
        [HttpDelete("{societyId:int}/events/{eventId:int}", Name = nameof(RemoveEvent))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RemoveEvent([FromRoute] int societyId, [FromRoute] int eventId)
        {
            var society = await Context.Societies
                .Include(s => s.Events)
                .SingleOrDefaultAsync(s => s.SocietyId == societyId);

            var anEvent = society?.Events.SingleOrDefault(e => e.EventId == eventId);

            if (society == null || anEvent == null)
            {
                return NotFound();
            }

            society.Events.Remove(anEvent);

            await Context.SaveChangesAsync();
            return NoContent();
        }

        // GET
        [HttpGet("{societyId:int}/events", Name = nameof(GetSocietyEvents))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetEventDto[]>> GetSocietyEvents([FromRoute] int societyId)
        {
            var events = await Context.Societies
                .AsNoTracking()
                .Include(s => s.Events)
                .ThenInclude(s => s.Event)
                .Where(s => s.SocietyId == societyId)
                .SelectMany(s => s.Events.Select(x => x.Event))
                .ProjectTo<GetEventDto>(Mapper.ConfigurationProvider)
                .ToArrayAsync();

            return Ok(events);
        }

        private bool SocietyExists(int id)
        {
            return Context.Societies.Any(e => e.SocietyId == id);
        }
    }
}
