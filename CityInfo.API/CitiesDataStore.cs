using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
  public class CitiesDataStore
  {
    //public static CitiesDataStore Current { get; } = new CitiesDataStore();

    public List<CityModel> Cities { get; set; }

    public CitiesDataStore()
    {
      Cities = new List<CityModel>()
      {
        new CityModel()
        {
          Id = 1,
          Name = "New York City",
          Description = "Big Apple",
          NumberOfPointsOfInterest = 10,
          PointsOfInterest = new List<PointOfInterestModel>()
          {
            new PointOfInterestModel()
            {
              Id = 1,
              Name = "Broadway",
              Description = "Phantom of the Opera is recommended",
            },
            new PointOfInterestModel()
            {
              Id = 2,
              Name = "Times Square",
              Description = "Crossroads of the City",
            },
            new PointOfInterestModel()
            {
              Id = 3,
              Name = "Central Park",
              Description = "Some greenery in the contrete jungle",
            },
          },
        },
        new CityModel()
        {
          Id = 2,
          Name = "Antwerp",
          Description = "Has a cathedral that was never finished",
          NumberOfPointsOfInterest = 3,
          PointsOfInterest = new List<PointOfInterestModel>()
          {
            new PointOfInterestModel()
            {
              Id = 4,
              Name = "Cathedral of Our Lady",
              Description = "Was supposed to have two towers, not just one",
            },
          },
        },
        new CityModel()
        {
          Id = 3,
          Name = "Paris",
          Description = "Avecs",
          NumberOfPointsOfInterest = 13,
          PointsOfInterest = new List<PointOfInterestModel>()
          {
            new PointOfInterestModel()
            {
              Id = 5,
              Name = "Eiffel Tower",
              Description = "Was originaly only supposed to be a temporary building",
            },
            new PointOfInterestModel()
            {
              Id = 6,
              Name = "Notre Dame",
              Description = "How is God going to protect you, if he can't even protect his own house",
            },
          },
        }
      };
    }
  }
}
