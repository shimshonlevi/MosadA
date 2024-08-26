
using Microsoft.EntityFrameworkCore;
using Mosad1.Data;
using Mosad1.Enums;
using Mosad1.Models;

namespace Mosad1.Models
{

    //public interface IMissionService
    //{

    //}
    public class missonServis 
        //IMissionService
    {
        private readonly Mosad1Context _context;
        private readonly DistanceCalculate _distanceCalculate;
        public missonServis(Mosad1Context context,DistanceCalculate distanceCalculate)
        {
            this._context = context;
            _distanceCalculate = distanceCalculate;


        }
        //public double GetDistance(Location location1, Location location2)
        //{
        //    return _distanceCalculate.CalculateDistance(location1, location2);
        //}
      
        public async Task CalculateMissionA(Agent agent)
        {

            //var targets = await _context.Targets.ToListAsync();
            var targets = await _context.Targets.Include(t => t.Location)?.ToListAsync();
            foreach (var target in targets)
            {
                if (target.Location == null)
                {
                    continue;
                }
                var distance = _distanceCalculate.CalculateDistance(agent.Location, target.Location);
                if (distance < 200)
                {
                   await CreateMission(agent, target);
                }
            }
        }
        public async Task CalculateMissionT(Target target)
        {
            //var agents = await _context.Agents.ToListAsync();
            var agents = await _context.Agents.Include(t => t.Location)?.ToListAsync();
            foreach (var agent in agents)
            {
                if(agent.Location == null)
                {
                    continue;
                }
                var distance = _distanceCalculate.CalculateDistance(agent.Location, target.Location);
                if (distance < 200)
                {
                    bool missionExists =  _context.Missions.Any(m => m.AgentID == agent.ID && m.TargetID == target.ID);
                    if (!missionExists)
                    {

                        await CreateMission(agent, target);
                    }
                }
            }
        }
        private async Task CreateMission(Agent agent, Target target)
        {
            
            var mission = new Mission
            {
                AgentID = agent.ID,
                TargetID = target.ID,
                TimeLeft = double.MaxValue,
                ExecutionTime = TimeOnly.MinValue,
                Status = StatusMission.Proposal,
            };
            this._context.Missions.Add(mission);
             await this._context.SaveChangesAsync();
        }
        public string MovingDirection(Location agentLoc, Location targetLoc)
        {
            int dirX = targetLoc.x - agentLoc.x;
            int dirY = targetLoc.y - agentLoc.y;
            string dir = "";
            if (dirY < 0) { dir += "n"; }
            if (dirY > 0) { dir += "s"; }
            if (dirX > 0) { dir += "e"; }
            if (dirX < 0) { dir += "w"; }
            return dir;
        }
    }

  
}
