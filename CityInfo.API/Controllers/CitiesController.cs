using CityInfo.API.Models;
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
      IEnumerable<CityModel> cities = _repo.GetAllCities()
        .Select(city => _modelFactory.Create(city));

      return Ok(cities);
    }

    [HttpGet, Route("{id:int}")]
    public IActionResult GetCity(int id)
    {
      City city = _repo.GetAllCities()
        .Where(c => c.Id == id)
        .FirstOrDefault();

      if (city == null)
        return NotFound("No City was found for the passed id");

      CityModel cityModel = _modelFactory.Create(city);

      return Ok(cityModel);
    }
  }
}
