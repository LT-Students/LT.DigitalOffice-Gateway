using System;

namespace LT.DigitalOffice.AdminService.Models.Db
{
  public class DbEndpointUrl
  {
    public const string TableName = "EndpointsUrls";

    public Guid Id { get; set; }
    public Guid EndpointId { get; set; }
    public string Url { get; set; }
  }
}
