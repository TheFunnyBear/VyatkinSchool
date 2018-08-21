using Microsoft.VisualStudio.TestTools.UnitTesting;
using VyatkinSchool.Controllers;
using System.Web.Mvc;

namespace VyatkinSchool.Tests
{
    [TestClass]
    public class HomeControllerModuleTest
    {
        [TestMethod]
        public void Contacts()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contacts() as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Can't create Contacts view");
        }
    }
}
