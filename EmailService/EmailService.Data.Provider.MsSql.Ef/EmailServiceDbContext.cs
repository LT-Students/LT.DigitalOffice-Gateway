using System.Reflection;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Models.Db;
using LT.DigitalOffice.Kernel.EFSupport.Provider;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.EmailService.Data.Provider.MsSql.Ef
{
  /// <summary>
  /// A class that defines the tables and its properties in the database of MessageService.
  /// </summary>
  public class EmailServiceDbContext : DbContext, IDataProvider
  {
    public EmailServiceDbContext(DbContextOptions<EmailServiceDbContext> options)
      : base(options)
    {
    }

    public DbSet<DbEmail> Emails { get; set; }
    public DbSet<DbUnsentEmail> UnsentEmails { get; set; }
    public DbSet<DbModuleSetting> ModuleSettings { get; set; }

    void IBaseDataProvider.Save()
    {
      SaveChanges();
    }

    async Task IBaseDataProvider.SaveAsync()
    {
      await SaveChangesAsync();
    }

    public void EnsureDeleted()
    {
      Database.EnsureDeleted();
    }

    public object MakeEntityDetached(object obj)
    {
      Entry(obj).State = EntityState.Detached;
      return Entry(obj).State;
    }

    public bool IsInMemory()
    {
      return Database.IsInMemory();
    }

    // Fluent API is written here.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("LT.DigitalOffice.EmailService.Models.Db"));
    }
  }
}
