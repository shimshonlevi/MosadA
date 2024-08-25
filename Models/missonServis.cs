
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
        public double GetDistance(Location loc1, Location loc2)
        {
            return _distanceCalculate.CalculateDistance(loc1, loc2);
        }
        // לנסות לחבר את הפונקציה של המרחק לללמטה
        public void CalculateMissionA(Agent agent)
        {
            var targets = _context.Targets.ToList();
            foreach (var target in targets)
            {
                var distance = _distanceCalculate.CalculateDistance(agent.Location, target.Location);
                if (distance < +200)
                {
                    CreateMission(agent, target);
                }
            }
        }
        public void CalculateMissionT(Target target)
        {
            var agents = _context.Agents.ToList();
            foreach (var agent in agents)
            {
                var distance = _distanceCalculate.CalculateDistance(agent.Location, target.Location);
                if (distance < +200)
                {
                    CreateMission(agent, target);
                }
            }
        }
        private void CreateMission(Agent agent, Target target)
        {
            var mission = new Mission
            {
                AgentID = agent.ID,
                TargetID = target.ID,
                TimeLeft = double.MaxValue,
                ExecutionTime = TimeOnly.MinValue,
                Status = StatusMission.Ready
            };
            this._context.Missions.Add(mission);
            this._context.SaveChangesAsync();
        }
    }

  
}
