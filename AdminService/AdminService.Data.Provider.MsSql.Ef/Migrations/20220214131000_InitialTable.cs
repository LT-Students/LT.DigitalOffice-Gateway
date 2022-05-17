using System;
using LT.DigitalOffice.AdminService.Models.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.AdminService.Data.Provider.MsSql.Ef.Migrations
{
  [DbContext(typeof(AdminServiceDbContext))]
  [Migration("20211123223000_InitialTable")]
  public class InitialTable : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: DbGraphicalUserInterfaceSetting.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          PortalName = table.Column<string>(nullable: true),
          LogoContent = table.Column<string>(nullable: true),
          LogoExtension = table.Column<string>(nullable: true),
          SiteUrl = table.Column<string>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbGraphicalUserInterfaceSetting.TableName}", x => x.Id);
        });

      migrationBuilder.CreateTable(
        name: DbServiceConfiguration.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          ServiceName = table.Column<string>(nullable: false),
          IsActive = table.Column<bool>(nullable: false),
          ModifiedBy = table.Column<Guid>(nullable: true),
          ModifiedAtUtc = table.Column<DateTime>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbServiceConfiguration.TableName}", x => x.Id);
        });

      migrationBuilder.CreateTable(
        name: DbServiceEndpoint.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          EndpointId = table.Column<Guid>(nullable: false),
          Locale = table.Column<string>(nullable: false),
          Name = table.Column<string>(nullable: false),
          Description = table.Column<string>(nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbServiceEndpoint.TableName}", x => x.Id);
        });

      migrationBuilder.CreateTable(
        name: DbEndpointUrl.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          EndpointId = table.Column<Guid>(nullable: false),
          Url = table.Column<string>(nullable: true),
        },
        constraints: table =>
        {
          table.PrimaryKey($"PK_{DbEndpointUrl.TableName}", x => x.Id);
        });

      migrationBuilder.InsertData(
      table: DbServiceConfiguration.TableName,
      columns: new[] { "Id", "ServiceName", "IsActive", "ModifiedBy", "ModifiedAtUtc" },
      columnTypes: new string[]
      {
        "uniqueidentifier",
        "nvarchar(max)",
        "bit",
        "uniqueidentifier",
        "datetime2"
      },
      values: new object[,] {
        { Guid.NewGuid(), "TimeService", true, null, null },
        { Guid.NewGuid(), "TaskService", true, null, null },
        { Guid.NewGuid(), "StreamService", true, null, null },
        { Guid.NewGuid(), "SearchService", true, null, null },
        { Guid.NewGuid(), "RightsService", true, null, null },
        { Guid.NewGuid(), "ProjectService", true, null, null },
        { Guid.NewGuid(), "PositionService", true, null, null },
        { Guid.NewGuid(), "OfficeService", true, null, null },
        { Guid.NewGuid(), "NewsService", true, null, null },
        { Guid.NewGuid(), "MessageService", true, null, null },
        { Guid.NewGuid(), "ImageService", true, null, null },
        { Guid.NewGuid(), "HistoryService", true, null, null },
        { Guid.NewGuid(), "FileService", true, null, null },
        { Guid.NewGuid(), "EducationService", true, null, null },
        { Guid.NewGuid(), "DepartmentService", true, null, null },
        { Guid.NewGuid(), "CompanyService", true, null, null },
        { Guid.NewGuid(), "AchievementService", true, null, null },
        { Guid.NewGuid(), "UserService", true, null, null }
      });
    }

    protected override void Down(MigrationBuilder builder)
    {
      builder.DropTable(
        name: DbGraphicalUserInterfaceSetting.TableName);

      builder.DropTable(
        name: DbServiceConfiguration.TableName);

      builder.DropTable(
        name: DbServiceEndpoint.TableName);
    }
  }
}
