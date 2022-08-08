using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;
using LT.DigitalOffice.FeedbackService.Models.Db;

namespace LT.DigitalOffice.FeedbackService.Data.Provider.MsSql.Ef
{
  public class FeedbackServiceDbContext : DbContext, IDataProvider
  {
    public DbSet<DbFeedback> Feedbacks { get; set; }
    public DbSet<DbFeedbackImage> FeedbacksImages { get; set; }

    public FeedbackServiceDbContext(DbContextOptions<FeedbackServiceDbContext> options)
      : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("LT.DigitalOffice.FeedbackService.Models.Db"));
    }

    public void Save()
    {
      SaveChanges();
    }

    public async Task SaveAsync()
    {
      await SaveChangesAsync();
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
  }
}
