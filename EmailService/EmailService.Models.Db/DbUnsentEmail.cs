﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.EmailService.Models.Db
{
  public class DbUnsentEmail
  {
    public const string TableName = "UnsentEmails";

    public Guid Id { get; set; }
    public Guid EmailId { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime LastSendAtUtc { get; set; }
    public uint TotalSendingCount { get; set; }

    public DbEmail Email { get; set; }
  }

  public class DbUnsentEmailConfiguration : IEntityTypeConfiguration<DbUnsentEmail>
  {
    public void Configure(EntityTypeBuilder<DbUnsentEmail> builder)
    {
      builder
        .ToTable(DbUnsentEmail.TableName);

      builder
        .HasKey(ue => ue.Id);

      builder
        .HasOne(ue => ue.Email)
        .WithOne(e => e.UnsentEmail);
    }
  }
}
