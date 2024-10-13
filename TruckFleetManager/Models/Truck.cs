using System.ComponentModel;
using System.Configuration;

namespace TruckFleetManager.Models
{
    public class Truck
    {
        public int TruckId { get; set; }
        
        public int Year { get; set; }
        
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Transmission { get; set; }


        [DisplayName("Last Service")]
        public DateOnly LastService {  get; set; }

        public string? Image {  get; set; }

        [DisplayName("TruckType")]
        //fk
        public int TruckTypeId { get; set; }

        // Parent ref
        public TruckType? TruckType { get; set; }


    }
}
