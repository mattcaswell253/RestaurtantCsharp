using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;


namespace BestRestaurants
{

    public class HomeModule : NancyModule
    {
      public HomeModule()
      {
        Get["/"] = _ =>
        {
            return View["index.cshtml"];
        };

        Get["/cuisines"] = _ =>
        {
            List<Cuisine> allCuisines = Cuisine.GetAll();
            return View["cuisines.cshtml", allCuisines];
        };

        Post["/cuisines"] = _ =>
        {
            Cuisine newthingie = new Cuisine(Request.Form["cuisine"]);
            newthingie.Save();
            List<Cuisine> allCuisines = Cuisine.GetAll();
            return View["cuisines.cshtml", allCuisines];
        };

        Get["/restaurants"] = _ =>
        {
            List<Rest> allRests = Rest.GetAll();
            return View["restaurants.cshtml", allRests];
        };

        Post["/restaurants"] = _ =>
        {
            Rest newthingie = new Rest(Request.Form["rest"], Request.Form["id_c"]);
            newthingie.Save();
            List<Rest> allRests = Rest.GetAll();
            return View["restaurants.cshtml", allRests];
        };

      }
    }
}
