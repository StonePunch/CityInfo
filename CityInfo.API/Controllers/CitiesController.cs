using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
  [Route("api/cities")]
  public class CitiesController : BaseController
  {
    [HttpGet, Route("")]
    public IActionResult GetCities()
    {
      return Ok(_repo.Cities);
    }

    [HttpGet, Route("{id:int}")]
    public IActionResult GetCity(int id)
    {
      CityModel cityModel = _repo.Cities
        .Where(city => city.Id == id)
        .FirstOrDefault();

      if (cityModel == null)
        return NotFound("No City was found for the passed id");

      return Ok(cityModel);
    }
  }
}
