using System.Reflection;
using System.Threading.Tasks;
using LT.DigitalOffice.AdminService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef
{
  public class AdminServiceDbContext : DbContext, IDataProvider
  {
    public AdminServiceDbContext(DbContextOptions<AdminServiceDbContext> options)
    : base(options)
    {
    }

    public DbSet<DbGraphicalUserInterfaceSetting> GraphicalUserInterfaceSettings { get; set; }
    public DbSet<DbServiceConfiguration> ServicesConfigurations { get; set; }
    public DbSet<DbServiceEndpoint> ServicesEndpoints { get; set; }
    public DbSet<DbEndpointUrl> EndpointsUrls { get; set; }

    // Fluent API is written here.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("LT.DigitalOffice.AdminService.Models.Db"));
    }

    public void Save()
    {
      SaveChanges();
    }

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;

      return Entry(obj).State;
    }

    public void EnsureDeleted()
    {
      Database.EnsureDeleted();
    }

    public bool IsInMemory()
    {
      return Database.IsInMemory();
    }

    public async Task SaveAsync()
    {
      await SaveChangesAsync();
    }
  }
}
