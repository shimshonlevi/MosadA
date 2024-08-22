namespace Mosad1.Models
{
    public class DirectionServis 
    {
        public static Location moveDurection(Target target, Direction direction)
        { 
            if (direction.direction == "nw") 
            {
                target.Location.y -= 1; target.Location.x -= 1;
            }
           
            else if (direction.direction == "n")
            {
                target.Location.y -= 1;
            }
           
            else if (direction.direction == "ne")
            {
                target.Location.y -= 1; target.Location.x -= 1;
            }
            else if (direction.direction == "w")
            {
                target.Location.x -= 1;
            }
            else if (direction.direction == "e")
            {
                target.Location.x += 1;
            }
            else if (direction.direction == "sw")
            {
                target.Location.y += 1; target.Location.x -= 1;
            }
            else if (direction.direction == "s")
            {
                target.Location.y += 1; 
            }
            else if (direction.direction == "se")
            {
                target.Location.y += 1; target.Location.x += 1;
            }
            return target.Location;

        }
        //public static readonly Dictionary<string, Action<Location>> DirectionActions = new Dictionary<string, Action<Location>>
        //{
        //    { "nw", loc => { loc.y -= 1; loc.x -= 1; } },
        //    { "n", loc => loc.y -= 1 },
        //    { "ne", loc => { loc.y -= 1; loc.x += 1; } },
        //    { "w", loc => loc.x -= 1 },
        //    { "e", loc => loc.x += 1 },
        //    { "sw", loc => { loc.y += 1; loc.x -= 1; } },
        //    { "s", loc => loc.y += 1 },
        //    { "se", loc => { loc.y += 1; loc.x += 1; } }
        //};

    }
}
