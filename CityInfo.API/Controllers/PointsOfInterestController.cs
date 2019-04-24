using CityInfo.API.Models;
using CityInfo.API.Properties;
using CityInfo.API.Services;
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
    private readonly IMailService _mailService;

    public PointsOfInterestController(IMailService mailService)
    {
      _mailService = mailService;
    }

    [HttpGet("")]
    public IActionResult GetPointsOfInterest(int cityId)
    {
      try
      {
        if (!_repo.CityExists(cityId))
          return NotFound();

        IEnumerable<PointOfInterest> pointsOfInterest = _repo.GetPointsOfInterestForCity(cityId);

        if (pointsOfInterest.Count() == 0)
          return NotFound();

        IEnumerable<PointOfInterestModel> pointsOfInterestModel = pointsOfInterest
          .Select(pointOfInterest => _modelFactory
          .CreatePointOfInterestModel(pointOfInterest));

        return Ok(pointsOfInterestModel);
      }
      catch (Exception exception)
      {
        _logger.LogCritical($"Exception while getting points of interest " +
          $"for city with the id:{cityId}", exception);
        return StatusCode(500, Resources.Http500Generic);
      }
    }

    [HttpGet("{id}", Name = "GetPointOfInterest")]
    public IActionResult GetPointOfInterest(int cityId, int id)
    {
      try
      {
        if (!_repo.CityExists(cityId))
          return NotFound();

        PointOfInterest pointOfInterest = _repo.GetPointOfInterestForCity(cityId, id);

        if (pointOfInterest == null)
          return NotFound();

        PointOfInterestModel pointOfInterestModel = _modelFactory
          .CreatePointOfInterestModel(pointOfInterest);

        return Ok(pointOfInterestModel);
      }
      catch (Exception exception)
      {
        _logger.LogCritical($"Exception while getting the point of interest " +
          $"with the id:{id} for city with the id:{cityId}", exception);
        return StatusCode(500, Resources.Http500Generic);
      }
    }

    [HttpPost("")]
    public IActionResult CreatePointOfInterest(int cityId, [FromBody]PointOfInterestModel pointOfInterestModel)
    {
      try
      {
        if (pointOfInterestModel == null)
          return BadRequest("The passed body could not be parsed into the appropriate object");

        if (pointOfInterestModel.Name == pointOfInterestModel.Description)
          ModelState.AddModelError("Description", "The provided description should be different from the name");

        if (!ModelState.IsValid)
          return BadRequest(ModelState);

        City city = _repo.GetCity(cityId, false);

        if (city == null)
          return NotFound();

        PointOfInterest pointOfInterest = new PointOfInterest()
        {
          Name = pointOfInterestModel.Name,
          Description = pointOfInterestModel.Description,
        };

        _repo.AddPointOfInterestToCity(cityId, pointOfInterest);

        if (!_repo.SaveChanges())
        {
          _logger.LogInformation($"Failed to save a new point of interest in the city with the id:{cityId}");
          return StatusCode(500, "A problem happened while saving the new entity");
        }

        return CreatedAtRoute("GetPointOfInterest", new
        {
          cityId = city.Id,
          id = pointOfInterest.Id,
        }, _modelFactory.CreatePointOfInterestModel(pointOfInterest));
      }
      catch (Exception exception)
      {
        _logger.LogCritical($"Exception while adding a point of interest " +
          $"to the city with the id:{cityId}", exception);
        return StatusCode(500, Resources.Http500Generic);
      }
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody]PointOfInterestModel pointOfInterestModel)
    {
      try
      {
        if (pointOfInterestModel == null)
          return BadRequest("The passed body could not be parsed into the appropriate object");

        if (pointOfInterestModel.Name == pointOfInterestModel.Description)
          ModelState.AddModelError("Description", "The provided description should be different from the name");

        if (!ModelState.IsValid)
          return BadRequest(ModelState);

        if (!_repo.CityExists(cityId))
          return NotFound();

        PointOfInterest pointOfInterest = _repo.GetPointOfInterestForCity(cityId, id);

        if (pointOfInterest == null)
          return NotFound();

        pointOfInterest.Name = pointOfInterestModel.Name;
        pointOfInterest.Description = pointOfInterestModel.Description;

        _repo.UpdatePointOfInterest(pointOfInterest);

        if (!_repo.SaveChanges())
        {
          _logger.LogInformation($"Failed to update a point of interest with " +
            $"the id:{pointOfInterest.Id} for the city with the id:{cityId}");
          return StatusCode(500, "A problem happened while updating the entity");
        }

        return NoContent();
      }
      catch (Exception exception)
      {
        _logger.LogCritical($"Exception while updating the point of interest " +
          $"for the city with the id:{cityId}", exception);
        return StatusCode(500, Resources.Http500Generic);
      }
      
    }

    [HttpPatch("{id}")]
    public IActionResult PartialUpdatePointOfInterest(int cityId, int id, [FromBody]JsonPatchDocument<PointOfInterestModel> patchDocument)
    {
      try
      {
        if (patchDocument == null)
          return BadRequest("The passed body could not be parsed into the appropriate object");

        if (!_repo.CityExists(cityId))
          return NotFound();

        PointOfInterest pointOfInterest = _repo.GetPointOfInterestForCity(cityId, id);

        if (pointOfInterest == null)
          return NotFound();

        PointOfInterestModel pointOfInterestModel = _modelFactory
          .CreatePointOfInterestModel(pointOfInterest);

        patchDocument.ApplyTo(pointOfInterestModel, ModelState);

        if (!ModelState.IsValid)
          return BadRequest(ModelState);

        if (pointOfInterestModel.Name == pointOfInterestModel.Description)
          ModelState.AddModelError("Description", "The provided description " +
            "should be different from the name");

        TryValidateModel(pointOfInterestModel);

        if (!ModelState.IsValid)
          return BadRequest(ModelState);

        pointOfInterest.Name = pointOfInterestModel.Name;
        pointOfInterest.Description = pointOfInterestModel.Description;

        _repo.UpdatePointOfInterest(pointOfInterest);

        if (!_repo.SaveChanges())
        {
          _logger.LogInformation($"Failed to update a point of interest with " +
            $"the id:{pointOfInterest.Id} in the city with the id:{cityId}");
          return StatusCode(500, "A problem happened while updating the entity");
        }

        return NoContent();
      }
      catch (Exception exception)
      {
        _logger.LogCritical($"Exception while updating the point of interest " +
          $"for the city with the id:{cityId}", exception);
        return StatusCode(500, Resources.Http500Generic);
      }
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePointOfInterest(int cityId, int id)
    {
      try
      {
        if (!_repo.CityExists(cityId))
          return NotFound();

        PointOfInterest pointOfInterest = _repo.GetPointOfInterestForCity(cityId, id);

        if (pointOfInterest == null)
          return NotFound();

        _repo.DeletePointOfInterest(id);

        if (!_repo.SaveChanges())
        {
          _logger.LogInformation($"Failed to delete the point of interest with " +
            $"the id:{pointOfInterest.Id} for the city with the id:{cityId}");
          return StatusCode(500, "A problem happened while deleting the entity");
        }

        // Send mail when a point of interest is deleted
        _mailService.Send("Point of interest deleted",
          $"Point of interest {pointOfInterest.Name} with id {pointOfInterest.Id} was deleted");

        return NoContent();
      }
      catch (Exception exception)
      {
        _logger.LogCritical($"Exception while deleting a point of interest for city with the id:{cityId}", exception);
        return StatusCode(500, Resources.Http500Generic);
      }
    }
  }
}
