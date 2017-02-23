using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
    public class RestTest : IDisposable
    {
        public RestTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void RestTest_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Rest.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void RestTest_ReturnsTrueIfDescrtptionsAreTheSame()
        {
            //Arrange, Act
            Rest firstRest = new Rest("Jimmy Johns", 1);
            Rest secondRest = new Rest("Jimmy Johns", 1);

            //Assert
            Assert.Equal(firstRest, secondRest);
        }

        [Fact]
        public void RestTest_Save_SavesToDatabase()
        {
            //Arrange
            Rest testRest = new Rest("Jimmy Johns", 1);

            //Act
            testRest.Save();
            List<Rest> result = Rest.GetAll();
            List<Rest> testList = new List<Rest>{testRest};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void RestTest_Save_AssignsIdToObject()
        {
            //Arrange
            Rest testRest = new Rest("Jimmy Johns", 1);

            //Act
            testRest.Save();
            Rest savedRest = Rest.GetAll()[0];

            int result = savedRest.GetId();
            int testId = testRest.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        public void Dispose()
        {
            Rest.DeleteAll();
        }
    }
}
