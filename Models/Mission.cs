using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mosad1.Models
{
    public class Mission
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Agent")]
        public int AgentID { get; set; }
        [ForeignKey("Target")]
        public int TargetID { get; set; }
        public double TimeLeft { get; set; }
        public string Status { get; set; }
    }
}
