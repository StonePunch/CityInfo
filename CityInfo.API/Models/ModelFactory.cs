using CityInfo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
  public class ModelFactory
  {
    public PointOfInterestModel Create(PointOfInterest pointOfInterest)
    {
      return new PointOfInterestModel()
      {
        Name = pointOfInterest.Name,
        Description = pointOfInterest.Description,
      };
    }

    public CityModel Create(City city)
    {
      ICollection<PointOfInterestModel> pointsOfInterest = city.PointsOfInterest
        .Select(pointOfInterest => Create(pointOfInterest)).ToList();

      return new CityModel()
      {
        Name = city.Name,
        Description = city.Description,
        //NumberOfPointsOfInterest = city.NumberOfPointsOfInterest,
        PointsOfInterest = pointsOfInterest,
      };
    }
  }
}
