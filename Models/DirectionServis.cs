namespace Mosad1.Models
{
    public class DirectionServis 
    {


        public static Location moveDurection(Location location, Direction direction)
         {
            if (direction.direction == "nw") 
            {
                location.y -= 1; location.x -= 1;
            }
           
            else if (direction.direction == "n")
            {
                location.y -= 1;
            }
           
            else if (direction.direction == "ne")
            {
                location.y -= 1; location.x -= 1;
            }
            else if (direction.direction == "w")
            {
                location.x -= 1;
            }
            else if (direction.direction == "e")
            {
                location.x += 1;
            }
            else if (direction.direction == "sw")
            {
                location.y += 1; location.x -= 1;
            }
            else if (direction.direction == "s")
            {
                location.y += 1; 
            }
            else if (direction.direction == "se")
            {
                location.y += 1; location.x += 1;
            }
            return location;

        }
      

    }
}
