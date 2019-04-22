using CityInfo.API.Models;
using CityInfo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
  public abstract class BaseController<T> : Controller where T : BaseController<T>
  {
    private ICitiesDataStore repo;

    protected ModelFactory _modelFactory { get; }

    private ILogger<T> logger;

    // TODO: Ask how this is working
    protected ICitiesDataStore _repo => repo ?? (repo = HttpContext?.RequestServices.GetService<ICitiesDataStore>());
    protected ILogger<T> _logger => logger ?? (logger = HttpContext?.RequestServices.GetService<ILogger<T>>());

    //public BaseController(ICitiesDataStore repo, ILogger<T> logger)
    //{
    //  _repo = repo;
    //  _modelFactory = new ModelFactory();
    //  _logger = logger;
    //}

    public BaseController()
    {
      _modelFactory = new ModelFactory();
    }
  }
}
