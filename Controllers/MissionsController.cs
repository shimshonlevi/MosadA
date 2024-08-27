using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgentManagementAPI.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mosad1.Data;
using Mosad1.Enums;
using Mosad1.Models;

namespace Mosad1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MissionsController : ControllerBase
    {
        private readonly Mosad1Context _context;
        private readonly missonServis _missionService;
        private readonly DistanceCalculate _distanceCalculate;

        public MissionsController(Mosad1Context context, missonServis missionService, DistanceCalculate distanceCalculate)
        {
            _context = context;
            _missionService = missionService;
            _distanceCalculate = distanceCalculate;
        }

        // GET: api/Missions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mission>>> GetMission()
        {
            return await _context.Missions.ToListAsync();
        }

        // GET: api/Missions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mission>> GetMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);

            if (mission == null)
            {
                return NotFound();
            }

            return mission;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> AgentToMission(int id,StatusMission statusMission)
        {
            int status;
            Mission? mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                status = StatusCodes.Status404NotFound;
                return NotFound(HttpUtils.Response(status, "mission not found"));
            }
            var agent = await _context.Agents.Include(a => a.Location).FirstOrDefaultAsync(a => a.ID == mission.AgentID);
            agent.Status = StatusAgent.InActive;
            mission.Status = StatusMission.InProgress;
            _context.Missions.Update(mission);
            await _context.SaveChangesAsync();
            status = StatusCodes.Status200OK;
            return StatusCode(status, HttpUtils.Response(status, new { mission = mission.StatusMission }));
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateTimeLeft()
        {
            int status = StatusCodes.Status200OK;
            var missions = await _context.Missions.ToListAsync();
            foreach (Mission mission in missions)
            {
                if (mission.Status != StatusMission.InProgress)
                {
                    continue;
                }

                    var agent = await _context.Agents.Include(a => a.Location).FirstOrDefaultAsync(a => a.ID == mission.AgentID);
                    var target = await _context.Targets.Include(t => t.Location).FirstOrDefaultAsync(t => t.ID == mission.TargetID);
                    if (agent == null || target == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "Agent or Target not found for mission");
                    }
                    mission.TimeLeft = _distanceCalculate.CalculateDistance(agent.Location, target.Location);
                    string MoveDirection = this._missionService.MovingDirection(agent.Location, target.Location);
                    if (MoveDirection.IsNullOrEmpty())
                    {
                        agent.Status = StatusAgent.Dormant;
                        target.status = StatusTarget.Eliminated;
                        target.Location = null;
                        mission.Status = StatusMission.Completed;
                        this._context.Agents.Update(agent);
                        this._context.Targets.Update(target);
                        this._context.Missions.Update(mission);
                    }
                    else
                    {
                        agent.Location = DirectionServis.moveDurection(agent.Location, new Direction { direction = MoveDirection });
                        this._context.Agents.Update(agent);
                    }
                    await this._context.SaveChangesAsync();

                
               
            }
            return StatusCode(
                status,
                HttpUtils.Response(status, new { missions = missions })
                );
        }


        // PUT: api/Missions/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMission(int id, Mission mission)
        //{
        //    if (id != mission.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(mission).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MissionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Missions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mission>> PostMission(Mission mission)
        {
            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMission", new { id = mission.ID }, mission);
        }

        // DELETE: api/Missions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return NotFound();
            }

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MissionExists(int id)
        {
            return _context.Missions.Any(e => e.ID == id);
        }
    }
}
