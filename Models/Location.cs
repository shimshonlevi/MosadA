using System.ComponentModel.DataAnnotations;

namespace Mosad1.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
