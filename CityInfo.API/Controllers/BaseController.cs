﻿using CityInfo.API.Models;
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
    private ICityInfoRepository repo;

    private ILogger<T> logger;

    protected ModelFactory _modelFactory { get; }

    // TODO: Ask how this is working
    protected ICityInfoRepository _repo => repo ?? (repo = HttpContext?.RequestServices.GetService<ICityInfoRepository>());
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
