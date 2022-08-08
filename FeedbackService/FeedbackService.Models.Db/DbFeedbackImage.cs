using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LT.DigitalOffice.FeedbackService.Models.Db
{
  public class DbFeedbackImage
  {
    public const string TableName = "FeedbacksImages";
    public Guid Id { get; set; }
    public Guid FeedbackId { get; set; }
    public Guid ImageId { get; set; }

    public DbFeedback Feedback { get; set; }
  }

  public class DbProjectImageConfiguration : IEntityTypeConfiguration<DbFeedbackImage>
  {
    public void Configure(EntityTypeBuilder<DbFeedbackImage> builder)
    {
      builder
        .ToTable(DbFeedbackImage.TableName);

      builder
        .HasKey(f => f.Id);

      builder
        .HasOne(fi => fi.Feedback)
        .WithMany(p => p.Images);
    }
  }
}
