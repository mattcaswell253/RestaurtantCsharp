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
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Rest.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    public void Dispose()
    {
      Rest.DeleteAll();
    }
  }
}
