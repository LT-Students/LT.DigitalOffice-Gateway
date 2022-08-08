using System;
using LT.DigitalOffice.FeedbackService.Data.Provider.MsSql.Ef;
using LT.DigitalOffice.FeedbackService.Models.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.DigitalOffice.OfficeService.Data.Migrations
{
  [DbContext(typeof(FeedbackServiceDbContext))]
  [Migration("20220802120000_InitialMigration")]
  public class InitialCreate : Migration
  {
    #region Create tables

    private static void CreateFeedbacksTable(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: DbFeedback.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          FeedbackType = table.Column<int>(nullable: false),
          Content = table.Column<string>(nullable: true),
          Status = table.Column<int>(nullable: false),
          SenderFullName = table.Column<string>(nullable: true),
          SenderIp = table.Column<string>(nullable: true),
          SenderId = table.Column<Guid>(nullable: false),
          CreatedAtUtc = table.Column<DateTime>(nullable: false),
          ChangedBy = table.Column<Guid>(nullable: true),
          ChangedAtUtc = table.Column<DateTime>(nullable: true)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Feedbacks", f => f.Id);
        });
    }

    private static void CreateFeedbacksImagesTable(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: DbFeedbackImage.TableName,
        columns: table => new
        {
          Id = table.Column<Guid>(nullable: false),
          FeedbackId = table.Column<Guid>(nullable: false),
          ImageId = table.Column<Guid>(nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_FeedbacksImages", fi => fi.Id);
        });
    }

    #endregion

    protected override void Up(MigrationBuilder migrationBuilder)
    {
      CreateFeedbacksTable(migrationBuilder);
      CreateFeedbacksImagesTable(migrationBuilder);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(DbFeedback.TableName);
      migrationBuilder.DropTable(DbFeedbackImage.TableName);
    }
  }
}
