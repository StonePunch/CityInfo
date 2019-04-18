using CityInfo.API.Models;
using CityInfo.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
  public abstract class BaseController : Controller
  {
    public CitiesDataStore _repo { get; }

    public ModelFactory _modelFactory { get; }

    public BaseController()
    {
      _repo = new CitiesDataStore();
      _modelFactory = new ModelFactory();
    }
  }
}
