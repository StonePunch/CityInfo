using CityInfo.Data;
using CityInfo.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
  public class CityInfoRepository : ICityInfoRepository
  {
    private readonly CityInfoContext _context;

    public CityInfoRepository(CityInfoContext context)
    {
      _context = context;
    }

    public IEnumerable<City> GetCities()
    {
      return _context.Cities
        .Include(city => city.PointsOfInterest)
        .OrderBy(city => city.Name)
        .ToList();
    }

    public City GetCity(int cityId, bool includePointsOfInterest)
    {
      if (includePointsOfInterest)
        return _context.Cities
          .Include(city => city.PointsOfInterest)
          .Where(city => city.Id == cityId)
          .FirstOrDefault();

      return _context.Cities
        .Where(city => city.Id == cityId)
        .FirstOrDefault();
    }

    public bool CityExists(int cityId)
    {
      return _context.Cities.Any(city => city.Id == cityId);
    }

    public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
    {
      return _context.PointsOfInterest
        .Where(pointOfInterest => pointOfInterest.CityId == cityId)
        .ToList();
    }

    public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
    {
      return _context.PointsOfInterest
        .Where(pointOfInterest => pointOfInterest.CityId == cityId && pointOfInterest.Id == pointOfInterestId)
        .FirstOrDefault();
    }

    public bool AddPointOfInterestToCity(int cityId, PointOfInterest pointOfInterest)
    {
      try
      {
        City city = GetCity(cityId, false);
        city.PointsOfInterest.Add(pointOfInterest);

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public bool UpdatePointOfInterest(PointOfInterest pointOfInterest)
    {
      try
      {
        EntityEntry<PointOfInterest> tracker = _context.PointsOfInterest.Attach(pointOfInterest);
        tracker.State = EntityState.Modified;

        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public bool DeletePointOfInterest(int pointOfInterestId)
    {
      try
      {
        PointOfInterest pointOfInterest = _context.PointsOfInterest
        .Where(p => p.Id == pointOfInterestId)
        .FirstOrDefault();

        if (pointOfInterest == null)
          return false;

        _context.PointsOfInterest.Remove(pointOfInterest);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public bool SaveChanges()
    {
      return _context.SaveChanges() >= 0;
    }
  }
}
