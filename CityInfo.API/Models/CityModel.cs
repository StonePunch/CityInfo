using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
  public class CityModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int NumberOfPointsOfInterest { get; set; }
    public IEnumerable<PointOfInterestModel> PointsOfInterest { get; set; } = 
      new List<PointOfInterestModel>();
  }
}
