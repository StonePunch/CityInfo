using CityInfo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CityInfo.Data
{
  public static class CityInfoContextExtensions
  {

    /// <summary>
    /// Seeding method
    /// </summary>
    public static void EnsureSeedDataForContext(this CityInfoContext context)
    {
      // Checking if any city already exists within the database in 
      // order to avoid data duplication
      if (context.Cities.Any())
        return;

      List<City> cities = new List<City>()
      {
        new City()
        {
          Name = "New York City",
          Description = "Big Apple",
          PointsOfInterest = new List<PointOfInterest>()
          {
            new PointOfInterest()
            {
              Name = "Broadway",
              Description = "Phantom of the Opera is recommended",
            },
            new PointOfInterest()
            {
              Name = "Times Square",
              Description = "Crossroads of the City",
            },
            new PointOfInterest()
            {
              Name = "Central Park",
              Description = "Some greenery in the contrete jungle",
            },
          },
        },
        new City()
        {
          Name = "Antwerp",
          Description = "Has a cathedral that was never finished",
          PointsOfInterest = new List<PointOfInterest>()
          {
            new PointOfInterest()
            {
              Name = "Cathedral of Our Lady",
              Description = "Was supposed to have two towers, not just one",
            },
          },
        },
        new City()
        {
          Name = "Paris",
          Description = "Avecs",
          PointsOfInterest = new List<PointOfInterest>()
          {
            new PointOfInterest()
            {
              Name = "Eiffel Tower",
              Description = "Was originaly only supposed to be a temporary building",
            },
            new PointOfInterest()
            {
              Name = "Notre Dame",
              Description = "How is God going to protect you, if he can't even protect his own house",
            },
          },
        }
      };

      context.Cities.AddRange(cities);

      context.SaveChanges();
    }
  }
}
