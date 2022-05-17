using LT.DigitalOffice.AdminService.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef.Configuration
{
  public class DbEndpointUrlConfiguration : IEntityTypeConfiguration<DbEndpointUrl>
  {
    public void Configure(EntityTypeBuilder<DbEndpointUrl> builder)
    {
      builder
        .ToTable(DbEndpointUrl.TableName);

      builder
        .HasKey(eu => eu.Id);
    }
  }
}
