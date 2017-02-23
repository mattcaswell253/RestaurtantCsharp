using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurants
{
    public class Rest
    {
        private int _id;
        private string _name;
        private int _cuisineId;

        public Rest(string Name, int CuisineId, int Id = 0)
        {
            _id = Id;
            _name = Name;
            _cuisineId = CuisineId;
        }

        public override bool Equals(System.Object otherRest)
        {
            if (!(otherRest is Rest))
            {
                return false;
            }
            else
            {
                Rest newRest = (Rest) otherRest;
                bool idEquality = this.GetId() == newRest.GetId();
                bool nameEquality = this.GetName() == newRest.GetName();
                bool cuisineIdEquality = this.GetCuisineId() == newRest.GetCuisineId();
                return (idEquality && nameEquality && cuisineIdEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public int GetId()
        {
            return _id;
        }
        public void SetId(int newId)
        {
            _id = newId;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetCuisineId()
        {
            return _cuisineId;
        }

        public void SetCuisineId(int newCuisineId)
        {
            _cuisineId = newCuisineId;
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
                int restId = rdr.GetInt32(0);
                string restName = rdr.GetString(1);
                int restCuisine = rdr.GetInt32(2);
                Rest newRest = new Rest(restName, restCuisine, restId);
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

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, id_cuisine) OUTPUT INSERTED.id VALUES (@RestName, @RestCuisineId);", conn);

            SqlParameter nameParameter = new SqlParameter();
            SqlParameter cuisineParameter = new SqlParameter();
            nameParameter.ParameterName = "@RestName";
            cuisineParameter.ParameterName = "@RestCuisineId";
            nameParameter.Value = this.GetName();
            cuisineParameter.Value = this.GetCuisineId();
            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(cuisineParameter);
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

        public static Rest Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants WHERE id = @RestId;", conn);
            SqlParameter restIdParameter = new SqlParameter();
            restIdParameter.ParameterName = "@RestId";
            restIdParameter.Value = id.ToString();
            cmd.Parameters.Add(restIdParameter);
            SqlDataReader rdr = cmd.ExecuteReader();

            int foundRestId = 0;
            string foundRestName = null;
            int foundRestCuisineId = 0;

            while(rdr.Read())
            {
                foundRestId = rdr.GetInt32(0);
                foundRestName = rdr.GetString(1);
                foundRestCuisineId = rdr.GetInt32(2);
            }
            Rest foundRest = new Rest(foundRestName, foundRestCuisineId, foundRestId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundRest;
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
