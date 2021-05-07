using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocietyManager.Data.Entities;
using SocietyManager.Models.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SocietyManager.Controllers
{
    public class EventsController : ApiControllerBase
    {
        // GET: api/<EventsController>
        [HttpGet(Name = nameof(GetEvents))]
        public async Task<IActionResult> GetEvents()
        {
            var events = await Context.Events
                .AsNoTracking()
                .ProjectTo<GetEventDto>(Mapper.ConfigurationProvider)
                .ToArrayAsync();

            return Ok(events);
        }

        // GET: api/<EventsController>/1
        [HttpGet("{id}", Name = nameof(GetEvent))]
        [ProducesResponseType(typeof(GetEventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            try
            {
                var anEvent = await Context.Events
                    .AsNoTracking()
                    .ProjectTo<GetEventDto>(Mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(e => e.EventId == id);

                if (anEvent == null)
                {
                    return NotFound();
                }

                return Ok(anEvent);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to get event");
            }
        }

        // PUT: api/<EventsController>/5
        [HttpPut("{id}", Name = nameof(PutEvent))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetEventDto>> PutEvent([FromRoute] int id, [FromBody] UpdateEventDto newEventModel)
        {
            try
            {
                var oldEvent = await Context.Events.FindAsync(id);
                if (oldEvent == null)
                {
                    return NotFound($"Event with id of {id} could not be found");
                }

                var updatedEvent = Mapper.Map(newEventModel, oldEvent);

                await Context.SaveChangesAsync();
                return Mapper.Map<GetEventDto>(updatedEvent);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound($"Event with id of {id} was not found");
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to update a event");
                }
            }
        }

        // POST: api/<EventsController>
        [HttpPost(Name = nameof(PostEvent))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<GetEventDto>> PostEvent([FromBody] CreateEventDto createEventModel)
        {
            try
            {
                var anEvent = await Context.Events.SingleOrDefaultAsync(e => e.Name == createEventModel.Name);

                if (anEvent != null)
                {
                    return BadRequest("An event with the same name already exists");
                }

                anEvent = Mapper.Map<Event>(createEventModel);

                Context.Events.Add(anEvent);
                await Context.SaveChangesAsync();
                var newEvent = Mapper.Map<GetEventDto>(anEvent);

                return CreatedAtAction(nameof(GetEvent), new { id = newEvent.EventId }, newEvent);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to create an event");
            }
        }

        // DELETE: api/<EventsController>/5
        [HttpDelete("{id}", Name = nameof(DeleteEvent))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            try
            {
                var anEvent = await Context.Events.FindAsync(id);
                if (anEvent == null)
                {
                    return NotFound($"Event with id of {id} was not found");
                }

                Context.Events.Remove(anEvent);
                await Context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete a event");
            }
        }
        private bool EventExists(int id)
        {
            return Context.Events.Any(e => e.EventId == id);
        }
    }
}
