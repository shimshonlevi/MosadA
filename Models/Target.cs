using System.ComponentModel.DataAnnotations;

namespace Mosad1.Models
{
    public class Target
    {
        //[Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string status { get; set; } 
    }
}
