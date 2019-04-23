namespace CityInfo.API.Services
{
  public interface IMailService
  {
    bool Send(string subject, string body);
  }
}