using CityInfo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityInfo.Data
{
  public class DataStore : IDataStore
  {
    public static DataStore Current { get; } = new DataStore();

    public List<City> Cities { get; set; }

    public DataStore()
    {
      Cities.AddRange(new List<City>()
      {
        new City()
        {
          Id = 1,
          Name = "New York City",
          Description = "Big Apple",
          NumberOfPointsOfInterest = 10,
        },
        new City()
        {
          Id = 2,
          Name = "Antwerp",
          Description = "Has a cathedral that was never finished",
          NumberOfPointsOfInterest = 3,
        },
        new City()
        {
          Id = 3,
          Name = "Paris",
          Description = "Avecs",
          NumberOfPointsOfInterest = 13,
        }
      });
    }
  }
}
