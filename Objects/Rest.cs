using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurants
{
  public class Rest
  {
    private string _name;
    private string _cuisine;

    public Rest(string Cuisine, string Name)
    {
      _name = Name;
      _cuisine = Cuisine;
    }

    public string GetName()
    {
      return _name;
    }
    public string GetCuisine()
    {
      return _cuisine;
    }
    public void SetCuisine(string newCuisine)
    {
      _cuisine = newCuisine;
    }
    public static List<Rest> GetAll()
    {
      List<Rest> allRests = new List<Rest>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        string restName = rdr.GetString(0);
        string restCuisine = rdr.GetString(1);
        Rest newRest = new Rest(restName, restCuisine);
        allRests.Add(newRest);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allRests;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
