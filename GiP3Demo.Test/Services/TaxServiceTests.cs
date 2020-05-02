using GiP3Demo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GiP3Demo.Test.Services
{
    [TestClass]
    public class TaxServiceTests
    {
        [TestMethod]
        public void TestMethod0Euro()
        {
            // ARRANGE
            TaxService svc = new TaxService();

            // ACT
            decimal result = svc.ComputeTax("BE", 0);

            // ASSERT
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestMethod100Euro()
        {
            // ARRANGE
            TaxService svc = new TaxService();

            // ACT
            decimal result = svc.ComputeTax("BEL", 100);

            // ASSERT
            Assert.AreEqual(21, result);
        }
    }
}
