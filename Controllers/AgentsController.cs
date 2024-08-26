using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mosad1.Data;
using Mosad1.Models;

namespace Mosad1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly Mosad1Context _context;
        private readonly missonServis _missonServis;

        public AgentsController(Mosad1Context context, missonServis missonServis)
        {
            _context = context;
            _missonServis = missonServis;
        }

        // GET: api/Agents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgent()
        {
            var agents = await _context.Agents.Include(t => t.Location)?.ToArrayAsync();
            return await _context.Agents.ToListAsync();
        }

        // GET: api/Agents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agent>> GetAgent(int id)
        {
            var agents = await _context.Agents.Include(t => t.Location)?.ToArrayAsync();
            var agent = await _context.Agents.FindAsync(id);

            if (agent == null)
            {
                return NotFound();
            }

            return agent;
        }

        // PUT: api/Agents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/pin")]
        public async Task<IActionResult> PutAgent(int id, Location location)
        {
            if (location == null)
            {
                return BadRequest();
            }
            var agents = await _context.Agents.Include(t => t.Location)?.ToArrayAsync();
            Agent agent = await _context.Agents.FindAsync(id);
            agent.Location = location;
            await Task.Run(async () =>
            {
                await this._missonServis.CalculateMissionA(agent);
            });
          
            _context.Update(agent);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
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
        [HttpPut("{id}/move")]
        public async Task<ActionResult<Agent>> MoveTarget(int id, Direction direction)
        {
            if (direction == null)
            {
                return BadRequest("Direction cannot be null.");
            }


            var agent = await _context.Agents.Include(t => t.Location).FirstOrDefaultAsync(t => t.ID == id);
            if (agent == null)
            {
                return NotFound();
            }


            var originalLocation = new { agent.Location.x, agent.Location.y };


            agent.Location = DirectionServis.moveDurection(agent.Location, direction);


            if (agent.Location.y > 1000 || agent.Location.x > 1000 || agent.Location.y < 0 || agent.Location.x < 0)
            {

                return BadRequest(new
                {
                    Message = "Cannot move target out of bounds.",
                    Location = originalLocation
                });
            }


            _context.Update(agent);
            try
            {
                this._missonServis.CalculateMissionA(agent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(
                StatusCodes.Status200OK,
                new { agent.Location.x, agent.Location.y }
            );
        }

        // POST: api/Agents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Agent>> PostAgent(Agent agent)
        {
            _context.Agents.Add(agent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgent", new { id = agent.ID }, agent);
        }

        // DELETE: api/Agents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            var agent = await _context.Agents.FindAsync(id);
            if (agent == null)
            {
                return NotFound();
            }

            _context.Agents.Remove(agent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.ID == id);
        }
    }
}
