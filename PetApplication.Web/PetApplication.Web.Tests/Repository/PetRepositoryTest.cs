using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PetApplication.Entity;
using PetApplication.Repository;
using Moq;
using PetApplication.Service;
using System.Linq;
using PetApplication.Utility;

namespace PetApplication.Web.Tests.Repository
{
    [TestClass]
    public class PetRepositoryTest
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
        [Ignore]
        public static List<PetOwnerModel> GetMockPetOwnerResult()
        {
            return new List<PetOwnerModel>
            {
                new PetOwnerModel { Name ="Bob",Gender = "Male", Age=23, Pets = GetMaleMockPetResult() },
                new PetOwnerModel { Name ="Jennifer",Gender = "Female", Age=18, Pets = GetFemaleMockPetResult()}
             };
        }
        [Ignore]
        public static List<PetOwnerModel> GetMockPetOwnerSingleNullPetArrayResult()
        {
            return new List<PetOwnerModel>
            {
                new PetOwnerModel { Name ="Bob",Gender = "Male", Age=23, Pets = null },
                new PetOwnerModel { Name ="Jennifer",Gender = "Female", Age=18, Pets = GetFemaleMockPetResult()}
             };
        }
        [Ignore]
        public static List<PetOwnerModel> GetMockPetOwnerBothNullPetArrayResult()
        {
            return new List<PetOwnerModel>
            {
                new PetOwnerModel { Name ="Bob",Gender = "Male", Age=23, Pets = null },
                new PetOwnerModel { Name ="Jennifer",Gender = "Female", Age=18, Pets =null}
             };
        }
        [Ignore]
        public static List<PetOwnerModel> GetMockPetOwnerSingleGenderNull()
        {
            return new List<PetOwnerModel>
            {
                new PetOwnerModel { Name ="Bob",Gender = null, Age=23, Pets = GetMaleMockPetResult() },
                new PetOwnerModel { Name ="Jennifer",Gender = null, Age=18, Pets = GetFemaleMockPetResult()}
             };
        }
        [Ignore]
        public static List<PetModel> GetMaleMockPetResult()
        {
            return new List<PetModel>
            {
                new PetModel {Name="Garfield",Type="Cat"},
                new PetModel {Name="Fido", Type="Dog" },
                new PetModel {Name="Tom", Type="Cat" },
                new PetModel {Name="Simba", Type="Cat" },
                new PetModel {Name="Nemo", Type="Fish" },
                new PetModel {Name="Sam", Type="Dog" },
                new PetModel {Name="Garfield", Type="Cat" }
            };
        }
        [Ignore]
        public static List<PetModel> GetFemaleMockPetResult()
        {
            return new List<PetModel>
            {
                new PetModel {Name="Rosy",Type="Cat"},
                new PetModel {Name="Fido", Type="Dog" },
                new PetModel {Name="Lucy", Type="Cat" },
                new PetModel {Name="Sweetie", Type="Cat" },
                new PetModel {Name="Nemo", Type="Fish" },
                new PetModel {Name="Sam", Type="Dog" },
                new PetModel {Name="Rosy", Type="Cat" }
            };
        }

        #endregion

        [TestMethod]
        public void TestGetPetNamesAccordingToGenderNotNull()
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerResult());
            PetRepository petRepository = new PetRepository(mock.Object);
            //Act
            var result = petRepository.GetPetNamesAccordingToGender();
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetPetNamesAccordingToGenderResultGroupCount()
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerResult());
            PetRepository petRepository = new PetRepository(mock.Object);
            //Act
            var result = petRepository.GetPetNamesAccordingToGender() as List<PetResultViewModel>;
            //Assert
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public void TestMaleGenderGroupAvailable()
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerResult());
            PetRepository petRepository = new PetRepository(mock.Object);
            //Act
            var result = petRepository.GetPetNamesAccordingToGender() as List<PetResultViewModel>;
            //Assert
            Assert.IsTrue(result.Any(prog => prog.Gender == Constant.MaleKey));
        }
        [TestMethod]
        public void TestMaleGenderGroupCountAsExpected()
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerResult());
            PetRepository petRepository = new PetRepository(mock.Object);
            //Act
            var result = petRepository.GetPetNamesAccordingToGender() as List<PetResultViewModel>;
            //Assert
            Assert.AreEqual(result.Count(prog => prog.Gender == Constant.MaleKey), 1);
        }

        [TestMethod]
        public void TestFemaleGenderGroupAvailable()
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerResult());
            PetRepository petRepository = new PetRepository(mock.Object);
            //Act
            var result = petRepository.GetPetNamesAccordingToGender() as List<PetResultViewModel>;
            //Assert
            Assert.IsTrue(result.Any(prog => prog.Gender == Constant.FemaleKey));
        }

        [TestMethod]
        public void TestFemaleGenderGroupCountAsExpected()
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerResult());
            PetRepository petRepository = new PetRepository(mock.Object);
            //Act
            var result = petRepository.GetPetNamesAccordingToGender() as List<PetResultViewModel>;
            //Assert
            Assert.AreEqual(result.Count(prog => prog.Gender == Constant.FemaleKey), 1);
        }

        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataTestMethod]
        public void TestOrderSequenceOfPetsForMaleOwner(int index)
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerResult());
            PetRepository petRepository = new PetRepository(mock.Object);

            //Act
            var result = petRepository.GetPetNamesAccordingToGender() as List<PetResultViewModel>;
            var petNames = result.Where(s => s.Gender == Constant.MaleKey).SelectMany(p => p.PetNames);
            var expectedResult = GetMockResult().Where(s => s.Gender == Constant.MaleKey).SelectMany(m => m.PetNames);
            //Assert
            Assert.AreEqual(petNames.ElementAt(index), expectedResult.ElementAt(index));

        }

        [TestMethod]
        public void TestNoGenderAvailableForGrouping()
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerSingleGenderNull());
            PetRepository petRepository = new PetRepository(mock.Object);

            //Act
            var result = petRepository.GetPetNamesAccordingToGender() as List<PetResultViewModel>;
            var genderGroupKey = result.Select(m => m.Gender).First();

            //Assert
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(genderGroupKey, null);
        }

        [TestMethod]
        public void TestNullPetJsonArrayExistsForSingleGroup()
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerSingleNullPetArrayResult());
            PetRepository petRepository = new PetRepository(mock.Object);

            //Act
            var result = petRepository.GetPetNamesAccordingToGender() as List<PetResultViewModel>;
            var petNamesMaleGroupJsonData = result.Where(m => m.Gender == Constant.MaleKey).SelectMany(m => m.PetNames);

            //Assert

            Assert.AreEqual(petNamesMaleGroupJsonData.Count(), 0);
        }

        [TestMethod]
        public void TestNullPetJsonArrayExistsForAllGroup()
        {
            //Arrange
            var mock = new Mock<IGetPetServiceData>();
            mock.Setup(p => p.GetPetDataFromService()).Returns(GetMockPetOwnerBothNullPetArrayResult());
            PetRepository petRepository = new PetRepository(mock.Object);
            //Act
            var result = petRepository.GetPetNamesAccordingToGender() as List<PetResultViewModel>;
            var petNamesMaleGroupJsonData = result.Where(m => m.Gender == Constant.MaleKey).SelectMany(m => m.PetNames);
            var petNamesFemaleGroupJsonData = result.Where(m => m.Gender == Constant.FemaleKey).SelectMany(m => m.PetNames);
            //Assert
            Assert.AreEqual(petNamesMaleGroupJsonData.Count(), 0);
            Assert.AreEqual(petNamesFemaleGroupJsonData.Count(), 0);
        }
    }
}
