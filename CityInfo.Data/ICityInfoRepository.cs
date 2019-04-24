using CityInfo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityInfo.Data
{
  public interface ICityInfoRepository
  {
    IEnumerable<City> GetCities();

    City GetCity(int cityId, bool includePointsOfInterest);

    bool CityExists(int cityId);

    IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);

    PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);

    bool AddPointOfInterestToCity(int cityId, PointOfInterest pointOfInterest);

    bool UpdatePointOfInterest(PointOfInterest pointOfInterest);

    bool DeletePointOfInterest(int pointOfInterestId);

    bool SaveChanges();
  }
}
