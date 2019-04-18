using CityInfo.API.Models;
using CityInfo.Data.Entities;
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
    [HttpGet("")]
    public IActionResult GetPointsOfInterest(int cityId)
    {
      City city = _repo.GetCity(cityId);

      if (city == null)
        return NotFound("No City was found for the passed id");

      CityModel cityModel = _modelFactory.Create(city);

      IEnumerable<PointOfInterestModel> pointsOfInterest = cityModel.PointsOfInterest;

      return Ok(pointsOfInterest);
    }

    [HttpGet("{id}", Name = "GetPointOfInterest")]
    public IActionResult GetPointOfInterest(int cityId, int id)
    {
      City city = _repo.GetCity(cityId);

      if (city == null)
        return NotFound("No City was found for the passed id");

      PointOfInterest pointOfInterest = city.PointsOfInterest
        .Where(p => p.Id == id)
        .FirstOrDefault();

      PointOfInterestModel pointOfInterestModel = _modelFactory.Create(pointOfInterest);

      if (pointOfInterestModel == null)
        return NotFound("No Point of Interest was found for the passed id");

      return Ok(pointOfInterestModel);
    }

    [HttpPost("")]
    public IActionResult CreatePointOfInterest(int cityId, [FromBody]PointOfInterestModel pointOfInterestModel)
    {
      if (pointOfInterestModel == null)
        return BadRequest("The passed body could not be parsed into the appropriate object");

      City city = _repo.GetCity(cityId);

      if (city == null)
        return NotFound("No City was found for the passed id");

      int maxId = _repo.GetAllCities()
        .SelectMany(c => c.PointsOfInterest)
        .Max(p => p.Id);

      PointOfInterest pointOfInterest = new PointOfInterest()
      {
        Id = ++maxId,
        Name = pointOfInterestModel.Name,
        Description = pointOfInterestModel.Description,
      };

      city.PointsOfInterest.ToList().Add(pointOfInterest);

      return CreatedAtRoute("GetPointOfInterest", new {
        cityId = city.Id,
        id = pointOfInterest.Id,
      }, pointOfInterest);
    }
  }
}
