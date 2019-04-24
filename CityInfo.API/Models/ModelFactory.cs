using CityInfo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
  public class ModelFactory
  {
    public PointOfInterestModel CreatePointOfInterestModel(PointOfInterest pointOfInterest)
    {
      return new PointOfInterestModel()
      {
        Name = pointOfInterest.Name,
        Description = pointOfInterest.Description,
      };
    }

    public CityModel CreateCityModel(City city)
    {
      IEnumerable<PointOfInterestModel> pointsOfInterest = city.PointsOfInterest
        .Select(pointOfInterest => CreatePointOfInterestModel(pointOfInterest));
      
      return new CityModel()
      {
        Name = city.Name,
        Description = city.Description,
        NumberOfPointsOfInterest = city.PointsOfInterest.Count,
        PointsOfInterest = pointsOfInterest,
      };
    }

    public CityWithoutPointsOfInterestModel CreateCityWithoutPointsOfInterestModel(City city)
    {
      return new CityWithoutPointsOfInterestModel()
      {
        Name = city.Name,
        Description = city.Description,
        NumberOfPointsOfInterest = city.PointsOfInterest.Count,
      };
    }
  }
}
