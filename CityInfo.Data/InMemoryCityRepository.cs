using CityInfo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Data
{
  public class InMemoryCityRepository : ICityInfoRepository
  {
    private readonly IEnumerable<City> Cities;
    
    public InMemoryCityRepository()
    {
      Cities = new List<City>()
      {
        new City()
        {
          Id = 1,
          Name = "New York City",
          Description = "Big Apple",
          PointsOfInterest = new List<PointOfInterest>()
          {
            new PointOfInterest()
            {
              Id = 1,
              CityId = 1,
              Name = "Broadway",
              Description = "Phantom of the Opera is recommended",
            },
            new PointOfInterest()
            {
              Id = 2,
              CityId = 1,
              Name = "Times Square",
              Description = "Crossroads of the City",
            },
            new PointOfInterest()
            {
              Id = 3,
              CityId = 1,
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
          PointsOfInterest = new List<PointOfInterest>()
          {
            new PointOfInterest()
            {
              Id = 4,
              CityId = 2,
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
          PointsOfInterest = new List<PointOfInterest>()
          {
            new PointOfInterest()
            {
              Id = 5,
              CityId = 3,
              Name = "Eiffel Tower",
              Description = "Was originaly only supposed to be a temporary building",
            },
            new PointOfInterest()
            {
              Id = 6,
              CityId = 3,
              Name = "Notre Dame",
              Description = "How is God going to protect you, if he can't even protect his own house",
            },
          },
        }
      };
    }

    public bool AddPointOfInterestToCity(int cityId, PointOfInterest pointOfInterest)
    {
      try
      {
        City city = Cities
          .Where(c => c.Id == cityId)
          .FirstOrDefault();

        city.PointsOfInterest.Add(pointOfInterest);

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public bool CityExists(int cityId)
    {
      return Cities.Any(city => city.Id == cityId);
    }

    public bool DeletePointOfInterest(int pointOfInterestId)
    {
      try
      {
        PointOfInterest pointOfInterest = Cities
          .SelectMany(c => c.PointsOfInterest)
          .Where(p => p.Id == pointOfInterestId)
          .FirstOrDefault();

        if (pointOfInterest == null)
          return false;

        Cities
          .Where(c => c.Id == pointOfInterest.CityId)
          .FirstOrDefault()
          .PointsOfInterest
          .Remove(pointOfInterest);

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public IEnumerable<City> GetCities()
    {
      return Cities
        .Select(city => city) // Make it a new list and not a direct reference to the inMemory list
        .OrderBy(city => city.Name)
        .ToList();
    }

    public City GetCity(int cityId, bool includePointsOfInterest)
    {
      if (includePointsOfInterest)
      {
        City city = Cities
          .Select(c => c) // Make it a new list and not a direct reference to the inMemory list
          .Where(c => c.Id == cityId)
          .FirstOrDefault();

        city.PointsOfInterest = null;

        return city;
      }
        
      return Cities
        .Select(city => city) // Make it a new list and not a direct reference to the inMemory list
        .Where(city => city.Id == cityId)
        .FirstOrDefault();
    }

    public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
    {
      return Cities
        .SelectMany(city => city.PointsOfInterest)
        .Where(pointOfInterest => pointOfInterest.CityId == cityId)
        .Where(pointOfInterest => pointOfInterest.Id == pointOfInterestId)
        .FirstOrDefault();
    }

    public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
    {
      return Cities
        .SelectMany(city => city.PointsOfInterest)
        .Where(pointOfInterest => pointOfInterest.CityId == cityId);
    }

    public bool SaveChanges()
    {
      return true;
    }

    public bool UpdatePointOfInterest(PointOfInterest pointOfInterest)
    {
      try
      {
        PointOfInterest pointOfInterestToChange = Cities
          .Where(city => city.Id == pointOfInterest.CityId)
          .FirstOrDefault()
          .PointsOfInterest
          .Where(p => p.Id == pointOfInterest.Id)
          .FirstOrDefault();

        pointOfInterestToChange.Name = pointOfInterest.Name;
        pointOfInterestToChange.Description = pointOfInterest.Description;

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }
}
