using CityInfo.API.Models;
using CityInfo.API.Properties;
using CityInfo.Data;
using CityInfo.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
  [Route("api/cities")]
  public class CitiesController : BaseController<CitiesController>
  {
    [HttpGet, Route("")]
    public IActionResult GetCities()
    {
      try
      {
        IEnumerable<City> cities = _repo.GetCities();

        if (cities.Count() == 0)
          return NotFound();

        IEnumerable<CityModel> citiesModel = cities
          .Select(city => _modelFactory.CreateCityModel(city));

        return Ok(citiesModel);
      }
      catch (Exception exception)
      {
        _logger.LogCritical("Exception while getting cities", exception);
        return StatusCode(500, Resources.Http500Generic);
      }
      
    }

    [HttpGet, Route("{id:int}")]
    public IActionResult GetCity(int id, bool includePointsOfInterest = false)
    {
      try
      {
        City city = _repo.GetCity(id, includePointsOfInterest);

        if (city == null)
          return NotFound();

        if (includePointsOfInterest)
          return Ok(_modelFactory.CreateCityModel(city));

        return Ok(_modelFactory.CreateCityWithoutPointsOfInterestModel(city));
      }
      catch (Exception exception)
      {
        _logger.LogCritical($"Exception while getting city with id:{id}", exception);
        return StatusCode(500, Resources.Http500Generic);
      }
      
    }
  }
}
