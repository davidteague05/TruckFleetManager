namespace TruckFleetManager.Models
{
    public class Truck
    {
        public int TruckId { get; set; }
        public string Brand { get; set; }

        public string Transmission { get; set; }

        public DateOnly LastService {  get; set; }


    }
}
