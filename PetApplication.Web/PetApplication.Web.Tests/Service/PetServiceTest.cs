using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetApplication.Service;
using PetApplication.Entity;

namespace PetApplication.Web.Tests.Service
{
    /// <summary>
    /// Summary description for PetServiceTest
    /// </summary>
    [TestClass]
    public class PetServiceTest
    {
        IGetPetServiceData _GetPetServiceData;
        #region Mock Data For Testing
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

        [TestCleanup]
        public void TestClean()
        {
            _GetPetServiceData = null;
        }

        [TestInitialize]
        public void TestInit()
        {
            _GetPetServiceData = new GetPetServiceData();
        }

        [TestMethod]
        public void TestPetServiceReturnsResult()
        {
            //Act
            var result = _GetPetServiceData.GetPetDataFromService();
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
