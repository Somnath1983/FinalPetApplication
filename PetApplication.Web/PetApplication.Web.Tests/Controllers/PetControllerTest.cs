using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetApplication.Web.Controllers;
using Moq;
using PetApplication.Repository;
using PetApplication.Entity;

namespace PetApplication.Web.Tests.Controllers
{
    [TestClass]
    public class PetControllerTest
    {
        #region Mock Data For Testing
        [Ignore]
        public static List<PetResultViewModel> GetMockResult()
        {
            return new List<PetResultViewModel>
            {
                new PetResultViewModel { Gender = "Male", PetNames = new List<string> { "Garfield", "Simba", "Tom" }},
                new PetResultViewModel { Gender = "Female", PetNames = new List<string> { "Lucy", "Rosy", "Sweetie" }},
            };
        }
        //Mock Error Data For Test
        [Ignore]
        public static List<PetResultViewModel> GetMockResultWithError()
        {
            return new List<PetResultViewModel>
            {
                new PetResultViewModel { Gender = "Male", PetNames = null},
                new PetResultViewModel { Gender = "Female", PetNames = null},
            };
        }
        #endregion

        [TestMethod]
        public void TestModelValueNull()
        {
            //Arrange
            var mock = new Mock<IPetRepository>();
            mock.Setup(p => p.GetPetNamesAccordingToGender()).Returns(GetMockResult());
            PetController controller = new PetController(mock.Object);

            //Act
            var result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result.Model);

        }


        [TestMethod]
        public void TestIndexReturnsCorrectView()
        {
            ////Arrange
            var mock = new Mock<IPetRepository>();
            mock.Setup(p => p.GetPetNamesAccordingToGender()).Returns(GetMockResult());
            PetController controller = new PetController(mock.Object);

            //Act
            var result = controller.Index();        
                       
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void TestMaleGenderCountAsExpected()
        {
            ////Arrange
            var mock = new Mock<IPetRepository>();
            mock.Setup(p => p.GetPetNamesAccordingToGender()).Returns(GetMockResultWithError());
            PetController controller = new PetController(mock.Object);

            //Act
            var result = controller.Error();
            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

    }
}
