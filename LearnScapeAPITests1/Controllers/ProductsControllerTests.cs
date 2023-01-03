using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace LearnScapeAPI.Controllers.Tests
{
    [TestClass()]
    public class ProductsControllerTests
    {
        private readonly StoreContext _context;

        public ProductsControllerTests(StoreContext context)
        {
            _context = context;
        }

        [TestMethod()]
        public void GetProductsTest()
        {
            Assert.Fail();
        }


        [TestMethod()]
        public void GetNotFoundRequest()
        {
            var thing = _context.Products.Find(42);

            if (thing == null)
            {
                Assert.Fail();
            }
        }
        
        [TestMethod()]
        public void GetServerError()
        {
            var thing = _context.Products.Find(42);
            var thingToReturn = thing.ToString();

            if (thingToReturn == null)
            {
                Assert.Fail();
            }

        }
    }
}