using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurants
{
  public class Cuisine
  {
    private int _id;
    private string _cuisine;

    public Cuisine(string Cuisine, int Id = 0)
    {
      _id = Id;
      _cuisine = Cuisine;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = this.GetId() == newCuisine.GetId();
        bool cuisineEquality = this.GetCuisine() == newCuisine.GetCuisine();
        return (idEquality && cuisineEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetCuisine().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetCuisine()
    {
      return _cuisine;
    }
    public void SetCuisine(string newCuisine)
    {
      _cuisine = newCuisine;
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int cuisineId = rdr.GetInt32(0);
        string cuisineCuisine = rdr.GetString(1);
        Cuisine newCuisine = new Cuisine(cuisineCuisine, cuisineId);
        allCuisines.Add(newCuisine);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allCuisines;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO cuisines (cuisine) OUTPUT INSERTED.id VALUES (@Cuisinename);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@Cuisinename";
      nameParameter.Value = this.GetCuisine();

      cmd.Parameters.Add(nameParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Cuisine Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM cuisines WHERE id = @CuisineId;", conn);
      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@CuisineId";
      cuisineIdParameter.Value = id.ToString();
      cmd.Parameters.Add(cuisineIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCuisineId = 0;
      string foundCuisineName = null;

      while(rdr.Read())
      {
        foundCuisineId = rdr.GetInt32(0);
        foundCuisineName = rdr.GetString(1);
      }
      Cuisine foundCuisine = new Cuisine(foundCuisineName, foundCuisineId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCuisine;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM cuisines;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
