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

    public BaseController()
    {
      _repo = new CitiesDataStore();
    }
  }
}
