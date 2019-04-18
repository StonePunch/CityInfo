using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.Data.Entities
{
  public class City
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int NumberOfPointsOfInterest { get; set; }
    public IEnumerable<PointOfInterest> PointsOfInterest { get; set; } = 
      new List<PointOfInterest>();
  }
}
