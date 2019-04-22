using System.Collections.Generic;
using CityInfo.Data.Entities;

namespace CityInfo.Data
{
  public interface ICitiesDataStore
  {
    ICollection<City> GetAllCities();

    City GetCity(int cityId);

    PointOfInterest Insert(int cityId, PointOfInterest pointOfInterest);

    bool Update(int cityId, PointOfInterest pointOfInterest);

    bool Delete(int cityId, PointOfInterest pointOfInterest);
  }
}