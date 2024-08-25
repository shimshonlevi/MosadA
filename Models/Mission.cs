using Mosad1.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mosad1.Models
{
    public class Mission
    {
        internal StatusMission StatusMission;

        [Key]
        public int ID { get; set; }
        [ForeignKey("Agent")]
        public int AgentID { get; set; }
        [ForeignKey("Target")]
        public int TargetID { get; set; }

        public TimeOnly ExecutionTime { get; set; }
        public double TimeLeft { get; set; }
        public StatusMission Status { get; set; }
    }
}
