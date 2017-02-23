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
        return View["cuisines.cshtml", ModelMaker()];
      };

      Post["/cuisines"] = _ =>
      {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine"]);
        newCuisine.Save();
        return View["cuisines.cshtml", ModelMaker()];
      };

      Get["/cuisines/{id}"]= parameters =>
      {
        Cuisine newCuisine = Cuisine.Find(parameters.id);
        Dictionary<string, object> model = ModelMaker();
        model.Add("Cuisine Object", newCuisine);
        model.Add("Restaurant List", Rest.GetByCuisine(newCuisine.GetId()));
        return View["cuisine.cshtml", model];

      };

      Get["/restaurants"] = _ =>
      {
        return View["restaurants.cshtml", ModelMaker()];
      };

      Post["/restaurants"] = _ =>
      {
        Rest newRest = new Rest(Request.Form["rest"], Request.Form["id_c"]);
        newRest.Save();
        return View["restaurants.cshtml", ModelMaker()];
      };
    }
    public static Dictionary<string, object> ModelMaker()
    {
      Dictionary<string, object> model = new Dictionary<string, object>{};
      model.Add("Cuisines", Cuisine.GetAll());
      model.Add("Restaurants", Rest.GetAll());
      return model;
    }


  }
}
