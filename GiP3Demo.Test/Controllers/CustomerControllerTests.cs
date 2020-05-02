using GiP3Demo.Controllers;
using GiP3Demo.Models;
using GiP3Demo.ViewModels.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiP3Demo.Test.Controllers
{
    [TestClass]
    public class CustomerControllerTests
    {
        [TestMethod]
        public void TestIndex_NoCustomers()
        {
            // ARRANGE
            var builder = new DbContextOptionsBuilder<AdventureWorks2017Context>();
            builder.UseInMemoryDatabase("AdventureWorks2017");
            AdventureWorks2017Context ctx = new AdventureWorks2017Context(builder.Options);

            CustomerController ctl = new CustomerController(ctx);

            // ACT
            IActionResult result = ctl.Index();

            // ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            ViewResult viewResult = (ViewResult)result;
            Assert.IsTrue(viewResult.Model is IEnumerable<CustomerViewModel>);
        }

        [TestMethod]
        public void TestDetails_Accountnumber_NonExisting()
        {
            // ARRANGE
            var builder = new DbContextOptionsBuilder<AdventureWorks2017Context>();
            builder.UseInMemoryDatabase("AdventureWorks2017");
            AdventureWorks2017Context ctx = new AdventureWorks2017Context(builder.Options);

            CustomerController ctl = new CustomerController(ctx);

            // ACT
            IActionResult result = ctl.Details(Guid.NewGuid().ToString());

            // ASSERT
            Assert.IsNotNull(result);
            Assert.IsTrue(result is NotFoundResult);
        }
    }
}
