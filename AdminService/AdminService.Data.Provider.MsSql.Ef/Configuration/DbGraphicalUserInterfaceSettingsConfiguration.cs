using LT.DigitalOffice.AdminService.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef.Configuration
{
  internal class DbGraphicalUserInterfaceSettingsConfiguration : IEntityTypeConfiguration<DbGraphicalUserInterfaceSetting>
  {
    public void Configure(EntityTypeBuilder<DbGraphicalUserInterfaceSetting> builder)
    {
      builder
        .ToTable(DbGraphicalUserInterfaceSetting.TableName);

      builder
        .HasKey(guis => guis.Id);

      builder
        .Property(guis => guis.SiteUrl)
        .IsRequired();
    }
  }
}
