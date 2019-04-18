using CityInfo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Data
{
  public class CitiesDataStore
  {
    private readonly List<City> Cities;

    public CitiesDataStore()
    {
      Cities = new List<City>()
      {
        new City()
        {
          Id = 1,
          Name = "New York City",
          Description = "Big Apple",
          NumberOfPointsOfInterest = 10,
          PointsOfInterest = new List<PointOfInterest>()
          {
            new PointOfInterest()
            {
              Id = 1,
              Name = "Broadway",
              Description = "Phantom of the Opera is recommended",
            },
            new PointOfInterest()
            {
              Id = 2,
              Name = "Times Square",
              Description = "Crossroads of the City",
            },
            new PointOfInterest()
            {
              Id = 3,
              Name = "Central Park",
              Description = "Some greenery in the contrete jungle",
            },
          },
        },
        new City()
        {
          Id = 2,
          Name = "Antwerp",
          Description = "Has a cathedral that was never finished",
          NumberOfPointsOfInterest = 3,
          PointsOfInterest = new List<PointOfInterest>()
          {
            new PointOfInterest()
            {
              Id = 4,
              Name = "Cathedral of Our Lady",
              Description = "Was supposed to have two towers, not just one",
            },
          },
        },
        new City()
        {
          Id = 3,
          Name = "Paris",
          Description = "Avecs",
          NumberOfPointsOfInterest = 13,
          PointsOfInterest = new List<PointOfInterest>()
          {
            new PointOfInterest()
            {
              Id = 5,
              Name = "Eiffel Tower",
              Description = "Was originaly only supposed to be a temporary building",
            },
            new PointOfInterest()
            {
              Id = 6,
              Name = "Notre Dame",
              Description = "How is God going to protect you, if he can't even protect his own house",
            },
          },
        }
      };
    }

    public List<City> GetAllCities()
    {
      return Cities;
    }

    public City GetCity(int id)
    {
      return Cities
        .Where(city => city.Id == id)
        .FirstOrDefault();
    }

    //public bool Insert(int cityId, PointOfInterest pointOfInterest)
    //{

    //}
  }
}
