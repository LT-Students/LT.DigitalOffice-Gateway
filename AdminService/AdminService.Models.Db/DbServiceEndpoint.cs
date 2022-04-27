using System;

namespace LT.DigitalOffice.AdminService.Models.Db
{
  public class DbServiceEndpoint
  {
    public const string TableName = "ServicesEndpoints";

    public Guid Id { get; set; }
    public Guid EndpointId { get; set; }
    public string Locale { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
  }
}
