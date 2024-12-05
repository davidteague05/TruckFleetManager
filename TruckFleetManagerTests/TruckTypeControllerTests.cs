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
            // Arrange
            controller.ModelState.AddModelError("Name", "The Name field is required.");
            var truckType = new TruckType 
            { 
                TruckTypeId = 1, 
                Name = "" 
            };

            // Act
            var result = (ViewResult)await controller.Create(truckType);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task CreateInvalidInputModelState()
        {
            // Arrange
            controller.ModelState.AddModelError("Name", "The Name field is required.");
            var truckType = new TruckType 
            { 
                TruckTypeId = 1, 
                Name = "" 
            };

            // Act
            var result = (ViewResult)await controller.Create(truckType);

            // Assert
            Assert.IsTrue(controller.ModelState.ContainsKey("Name"));
        }

        [TestMethod]
        public async Task CreateValidInput()
        {
            // Arrange
            var truckType = new TruckType 
            { 
                TruckTypeId = 1, 
                Name = "Flatbed" 
            };

            // Act
            var result = (RedirectToActionResult)await controller.Create(truckType);

            // Assert
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public async Task CreateValidInputCount()
        {
            // Arrange
            var truckType = new TruckType 
            { 
                TruckTypeId = 1, 
                Name = "Flatbed" 
            };

            // Act
            await controller.Create(truckType);

            // Assert
            Assert.AreEqual(1, _context.Type.Count());
        }

        [TestMethod]
        public async Task CreateValidInputName()
        {
            // Arrange
            var truckType = new TruckType 
            { 
                TruckTypeId = 1, 
                Name = "Flatbed" 
            };

            // Act
            await controller.Create(truckType);

            // Assert
            Assert.AreEqual("Flatbed", _context.Type.First().Name);
        }
    }
}