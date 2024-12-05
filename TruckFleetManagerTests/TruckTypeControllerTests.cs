using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TruckFleetManager.Controllers;
using TruckFleetManager.Data;
using TruckFleetManager.Models;

namespace TruckFleetManagerTests
{
    [TestClass]
    public class TruckTypeControllerTests
    {            
        private ApplicationDbContext _context;
        TruckTypesController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            controller = new TruckTypesController(_context);
        }

        [TestMethod]
        public void CreateReturnsView()
        {
            var result = (ViewResult)controller.Create();

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public async Task CreateInvalidInput()
        {
            controller.ModelState.AddModelError("Name", "The Name field is required.");
            
            var truckType = new TruckType
            {
                TruckTypeId = 1,
                Name = ""
            };

            var result = (ViewResult)await controller.Create(truckType);

            Assert.IsNotNull(result);
            Assert.AreEqual(null, result.ViewName);
            var model = (TruckType)result.Model;
            Assert.AreEqual(truckType, model);
            Assert.IsTrue(controller.ModelState.ContainsKey("Name")); 
            Assert.AreEqual(0, _context.Type.Count());

        }

        [TestMethod]
        public async Task CreateValidInput()
        {
            var truckType = new TruckType
            {
                TruckTypeId = 1,
                Name = "Flatbed"
            };

            var result = (RedirectToActionResult)await controller.Create(truckType);

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual(1, _context.Type.Count());
            Assert.AreEqual("Flatbed", _context.Type.First().Name);
        }
    }
}