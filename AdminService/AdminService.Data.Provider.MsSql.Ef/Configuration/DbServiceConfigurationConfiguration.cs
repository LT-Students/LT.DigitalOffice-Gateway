using LT.DigitalOffice.AdminService.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef.Configuration
{
  public class DbServiceConfigurationConfiguration : IEntityTypeConfiguration<DbServiceConfiguration>
  {
    public void Configure(EntityTypeBuilder<DbServiceConfiguration> builder)
    {
      builder
        .ToTable(DbServiceConfiguration.TableName);

      builder
        .HasKey(sc => sc.Id);

      builder
        .Property(sc => sc.ServiceName)
        .IsRequired();
    }
  }
}
