using Microsoft.AspNetCore.Mvc;
using TruckFleetManager.Data;

namespace TruckFleetManager.Controllers
{
    public class CompanyTrucksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CompanyTrucksController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var truckTypes = _context.Type.ToList();
            return View(truckTypes);
        }

        public IActionResult ByTruckType(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }

            var trucks = _context.Truck.Where(t => t.TruckId == id).ToList();

            return View(trucks);
        }
    }
}
