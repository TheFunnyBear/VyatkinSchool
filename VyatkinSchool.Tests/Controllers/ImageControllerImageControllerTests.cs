using NUnit.Framework;
using VyatkinSchool.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VyatkinSchool.Controllers.Tests
{
    [TestFixture]
    public class ImageControllerTests
    {
        [Test]
        public void ShowCheckTest()
        {
            var imageController = new ImageController();

            int existedID = 1;
            var actionResult =  imageController.Show(existedID);

            Assert.IsNotNull(actionResult, $"Can't show picture with existed id [{existedID}].");
        }
    }
}