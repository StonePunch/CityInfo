using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
  public class CloudMailService : IMailService
  {
    private readonly string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
    private readonly string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

    public bool Send(string subject, string body)
    {
      Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with CloudMailService");
      Debug.WriteLine($"Subject: {subject}");
      Debug.WriteLine($"Body: {body}");

      return true;
    }
  }
}
