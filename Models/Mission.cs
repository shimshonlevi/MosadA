using System.ComponentModel.DataAnnotations;

namespace Mosad1.Models
{
    public class Mission
    {
        [Key]
        public int ID { get; set; }
        public int AgentID { get; set; }
        public Agent Agent { get; set; }
        public int TargetID { get; set; }
        public Target Target { get; set; }
        public double TimeLeft { get; set; }
        public string Status { get; set; }
    }
}
