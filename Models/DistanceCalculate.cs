namespace Mosad1.Models
{
    public class DistanceCalculate
    {
        public double CalculateDistance(Location loc1, Location loc2)
        {
            return Math.Sqrt(Math.Pow(loc2.x - loc1.x, 2) + Math.Pow(loc2.y - loc1.y, 2));
        }
    }
}
