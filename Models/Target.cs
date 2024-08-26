using Mosad1.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mosad1.Models
{
    public class Target
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public Location ? Location { get; set; }
        public StatusTarget? status { get; set; } 
    }
}
