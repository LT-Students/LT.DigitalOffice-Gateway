﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.EmailService.Models.Db
{
  public class DbEmail
  {
    public const string TableName = "Emails";

    public Guid Id { get; set; }
    public Guid? SenderId { get; set; }
    public string Receiver { get; set; }
    public string Subject { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    public DbUnsentEmail UnsentEmail { get; set; }
  }

  public class DbEmailConfiguration : IEntityTypeConfiguration<DbEmail>
  {
    public void Configure(EntityTypeBuilder<DbEmail> builder)
    {
      builder
        .ToTable(DbEmail.TableName);

      builder
        .HasKey(e => e.Id);

      builder
        .Property(e => e.Receiver)
        .IsRequired();

      builder
        .Property(e => e.Subject)
        .IsRequired();

      builder
        .Property(e => e.Text)
        .IsRequired();

      builder
        .HasOne(e => e.UnsentEmail)
        .WithOne(ue => ue.Email);
    }
  }
}
