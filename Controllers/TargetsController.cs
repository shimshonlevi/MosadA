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
    public class TargetsController : ControllerBase
    {
        private readonly Mosad1Context _context;
        //private DirectionServis _directionServis;

        public TargetsController(Mosad1Context context)
        {
            _context = context;
            //_directionServis = directionServis;
        }

        // GET: api/Targets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Target>>> GetTarget()
        {
            var targets = await _context.Target.Include(t => t.Location)?.ToArrayAsync();
            return await _context.Target.ToListAsync();
        }

        // GET: api/Targets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Target>> GetTarget(int id)
        {
            var targets = await _context.Target.Include(t => t.Location)?.ToArrayAsync();
            var target = await _context.Target.FindAsync(id);

            if (target == null)
            {
                return NotFound();
            }

            return target;
        }

        // PUT: api/Targets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/pin")]
        public async Task<IActionResult> PutTarget(int id, Location location)
        {
            if (location == null)
            {
                return BadRequest();
            }
            var targets = await _context.Target.Include(t => t.Location)?.ToArrayAsync();
            Target target = await _context.Target.FindAsync(id);
            target.Location = location;
            
            _context.Update(target);
           

            try
            {
                await _context.SaveChangesAsync();
      
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetExists(id))
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

        // POST: api/Targets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Target>> PostTarget(Target target)
        {
            _context.Target.Add(target);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarget", new { id = target.ID });
        }
        [HttpPut("{id}/move")]
        public async Task<ActionResult<Target>> MoveTarget(int id, Direction direction)
        {
            if (direction == null)
            {
                return BadRequest("Direction cannot be null.");
            }

          
            var target = await _context.Target.Include(t => t.Location).FirstOrDefaultAsync(t => t.ID == id);
            if (target == null)
            {
                return NotFound();
            }

          
            var originalLocation = new { target.Location.x, target.Location.y };

        
            target.Location = DirectionServis.moveDurection(target.Location, direction);

         
            if (target.Location.y > 1000 || target.Location.x > 1000 || target.Location.y < 0 || target.Location.x < 0)
            {
       
                return BadRequest(new
                {
                    Message = "Cannot move target out of bounds.",
                    Location = originalLocation
                });
            }

      
            _context.Update(target);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetExists(id))
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
                new { target.Location.x, target.Location.y }
            );
        }



        // DELETE: api/Targets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarget(int id)
        {
            var target = await _context.Target.FindAsync(id);
            if (target == null)
            {
                return NotFound();
            }

            _context.Target.Remove(target);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TargetExists(int id)
        {
            return _context.Target.Any(e => e.ID == id);
        }
    }
}
