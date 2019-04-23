using CityInfo.API.Services;
using CityInfo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
  public class Startup
  {
    #region Creating global access to the config file

    public static IConfiguration Configuration { get; private set; }

    public Startup(IHostingEnvironment env)
    {
      IConfigurationBuilder configBuilder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

      Configuration = configBuilder.Build();
    }

    #endregion

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc()
        .AddMvcOptions(options =>
        {
          // Add a xml format serializer
          options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
        });

      services.AddSingleton<ICitiesDataStore, CitiesDataStore>();

#if DEBUG
      services.AddTransient<IMailService, LocalMailService>();
#else
      services.AddTransient<IMailService, CloudMailService>();
#endif

      /* Make it so that the names of the fields don't change when the models are serialized */
      //services.AddMvc()
      //  .AddJsonOptions(options => {
      //    if (options.SerializerSettings.ContractResolver != null)
      //    {
      //      DefaultContractResolver castedResolver = (DefaultContractResolver)options.SerializerSettings.ContractResolver;
      //      castedResolver.NamingStrategy = null;
      //    }
      //  });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole();
      loggerFactory.AddDebug(); // Displays loggind in the output window

      /* 3º party logging nugget "NLog"
       *
       * Adds new methods to the already implemented "ILogger" interface
       * Documentation:
       * https://github.com/NLog/NLog.Web/wiki/Getting-started-with-ASP.NET-Core-2
       * 
       */
      loggerFactory.AddNLog();

      if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();
      else
        app.UseExceptionHandler();

      app.UseStatusCodePages();

      app.UseMvc();

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }
  }
}
