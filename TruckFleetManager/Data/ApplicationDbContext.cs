using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TruckFleetManager.Models;

namespace TruckFleetManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TruckFleetManager.Models.TruckType> Type { get; set; } = default!;
        public DbSet<TruckFleetManager.Models.Truck> Truck { get; set; } = default!;
    }
}
