﻿namespace TruckFleetManager.Models
{
    public class TruckType
    {
        public int TruckTypeId { get; set; }
        public string Name { get; set; }

        public List<Truck>? Trucks { get; set; }
    }
}
