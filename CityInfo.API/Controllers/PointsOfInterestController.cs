using CityInfo.API.Models;
using CityInfo.Data;
using CityInfo.Data.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
  [Route("api/cities/{cityId:int}/pointsofinterest")]
  public class PointsOfInterestController : BaseController<PointsOfInterestController>
  {
    [HttpGet("")]
    public IActionResult GetPointsOfInterest(int cityId)
    {
      try
      {
        City city = _repo.GetCity(cityId);

        if (city == null)
        {
          _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
          return NotFound("No City was found for the passed id");
        }

        CityModel cityModel = _modelFactory.Create(city);

        IEnumerable<PointOfInterestModel> pointsOfInterest = cityModel.PointsOfInterest;

        return Ok(pointsOfInterest);
      }
      catch (Exception exception)
      {
        _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}", exception);
        return StatusCode(500, "A problem happened while handling your request");
      }
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

      if (pointOfInterestModel.Name == pointOfInterestModel.Description)
        ModelState.AddModelError("Description", "The provided description should be different from the name");

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      City city = _repo.GetCity(cityId);

      if (city == null)
        return NotFound("No City was found for the passed id");

      PointOfInterest pointOfInterest = new PointOfInterest()
      {
        Name = pointOfInterestModel.Name,
        Description = pointOfInterestModel.Description,
      };

      _repo.Insert(cityId, pointOfInterest);

      return CreatedAtRoute("GetPointOfInterest", new {
        cityId = city.Id,
        id = pointOfInterest.Id,
      }, pointOfInterest);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody]PointOfInterestModel pointOfInterestModel)
    {
      if (pointOfInterestModel == null)
        return BadRequest("The passed body could not be parsed into the appropriate object");

      if (pointOfInterestModel.Name == pointOfInterestModel.Description)
        ModelState.AddModelError("Description", "The provided description should be different from the name");

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      City city = _repo.GetCity(cityId);

      if (city == null)
        return NotFound("No City was found for the passed id");

      PointOfInterest pointOfInterest = city.PointsOfInterest
        .Where(p => p.Id == id)
        .FirstOrDefault();

      if (pointOfInterest == null)
        return NotFound("No Point of Interest was found for the passed id");

      pointOfInterest.Name = pointOfInterestModel.Name;
      pointOfInterest.Description = pointOfInterestModel.Description;

      _repo.Update(cityId, pointOfInterest);

      return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult PartialUpdatePointOfInterest(int cityId, int id, [FromBody]JsonPatchDocument<PointOfInterestModel> patchDocument)
    {
      if (patchDocument == null)
        return BadRequest("The passed body could not be parsed into the appropriate object");

      City city = _repo.GetCity(cityId);

      if (city == null)
        return NotFound("No City was found for the passed id");

      PointOfInterest pointOfInterest = city.PointsOfInterest
        .Where(p => p.Id == id)
        .FirstOrDefault();

      if (pointOfInterest == null)
        return NotFound("No Point of Interest was found for the passed id");

      PointOfInterestModel pointOfInterestModel = new PointOfInterestModel()
      {
        Name = pointOfInterest.Name,
        Description = pointOfInterest.Description,
      };

      patchDocument.ApplyTo(pointOfInterestModel, ModelState);

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      if (pointOfInterestModel.Name == pointOfInterestModel.Description)
        ModelState.AddModelError("Description", "The provided description should be different from the name");

      TryValidateModel(pointOfInterestModel);

      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      pointOfInterest.Name = pointOfInterestModel.Name;
      pointOfInterest.Description = pointOfInterestModel.Description;

      _repo.Update(cityId, pointOfInterest);

      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePointOfInterest(int cityId, int id)
    {
      City city = _repo.GetCity(cityId);

      if (city == null)
        return NotFound("No City was found for the passed id");

      PointOfInterest pointOfInterest = city.PointsOfInterest
        .Where(p => p.Id == id)
        .FirstOrDefault();

      if (pointOfInterest == null)
        return NotFound("No Point of Interest was found for the passed id");

      _repo.Delete(cityId, pointOfInterest);

      return NoContent();
    }
  }
}
