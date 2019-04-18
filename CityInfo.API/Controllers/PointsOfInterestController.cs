using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
  [Route("api/cities/{cityId:int}/pointsofinterest")]
  public class PointsOfInterestController : BaseController
  {
    [HttpGet, Route("")]
    public IActionResult GetPointsOfInterest(int cityId)
    {
      CityModel cityModel = _repo.Cities.Where(city => city.Id == cityId).FirstOrDefault();

      if (cityModel == null)
        return NotFound("No City was found for the passed id");

      IEnumerable<PointOfInterestModel> pointsOfInterest = cityModel.PointsOfInterest;

      return Ok(pointsOfInterest);
    }

    [HttpGet, Route("{id}")]
    public IActionResult GetPointOfInterest(int cityId, int id)
    {
      CityModel cityModel = _repo.Cities.Where(city => city.Id == cityId).FirstOrDefault();

      if (cityModel == null)
        return NotFound("No City was found for the passed id");

      PointOfInterestModel pointOfInterestModel = cityModel.PointsOfInterest
        .Where(pointOfInterest => pointOfInterest.Id == id)
        .FirstOrDefault();

      if (pointOfInterestModel == null)
        return NotFound("No Point of Interest was found for the passed id");

      return Ok(pointOfInterestModel);
    }
  }
}
