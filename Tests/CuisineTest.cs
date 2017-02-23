using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurants
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void CuisineTest_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Cuisine.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void CuisineTest_ReturnsTrueIfDescrtptionsAreTheSame()
    {
      //Arrange, Act
      Cuisine firstCuisine = new Cuisine("Sandwich Shop");
      Cuisine secondCuisine = new Cuisine("Sandwich Shop");

      //Assert
      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void CuisineTest_Save_SavesToDatabase()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Sandwhich Shop");

      //Act
      testCuisine.Save();
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      Assert.Equal(testList, result);

    }

    [Fact]
    public void CuisineTest_Save_AssignsIdToObject()
    {
        //Arrange
        Cuisine testCuisine = new Cuisine("Jimmy Johns", 1);

        //Act
        testCuisine.Save();
        Cuisine savedCuisine = Cuisine.GetAll()[0];

        int result = savedCuisine.GetId();
        int testId = testCuisine.GetId();

        //Assert
        Assert.Equal(testId, result);
    }

    public void Dispose()
    {
      Cuisine.DeleteAll();
    }
  }
}
