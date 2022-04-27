using LT.DigitalOffice.AdminService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Database;
using LT.DigitalOffice.Kernel.Enums;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.AdminService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    DbSet<DbGraphicalUserInterfaceSetting> GraphicalUserInterfaceSettings { get; set; }
    DbSet<DbServiceConfiguration> ServicesConfigurations { get; set; }
    DbSet<DbServiceEndpoint> ServicesEndpoints { get; set; }
    DbSet<DbEndpointUrl> EndpointsUrls { get; set; }
  }
}
