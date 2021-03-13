using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleMongoCrud.Controllers;
using SampleMongoCrud.Services;
using SampleMongoCrud.Models;

namespace SampleMongoCrudTest
{
    [TestClass]
    public class BooksControllerTest
    {
        public BooksController controller = new BooksController(true);
        [TestMethod]
        public void TestMethod1()
        {
            var r = controller.BookById("id");

            Assert.AreEqual(
                JsonSerializer.Serialize(new Book()),
                JsonSerializer.Serialize((Book)((OkObjectResult)r).Value)
                );
        }
    }
}
