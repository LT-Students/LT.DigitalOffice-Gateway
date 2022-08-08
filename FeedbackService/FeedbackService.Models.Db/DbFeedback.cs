using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using LT.DigitalOffice.Kernel.BrokerSupport.Attributes.ParseEntity;

namespace LT.DigitalOffice.FeedbackService.Models.Db
{
  public class DbFeedback
  {
    public const string TableName = "Feedbacks";
    public Guid Id { get; set; }
    public int FeedbackType { get; set; }
    public string Content { get; set; }
    public int Status { get; set; }
    public string SenderFullName { get; set; }
    public Guid SenderId { get; set; }
    public string SenderIp { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public Guid? ChangedBy { get; set; }
    public DateTime? ChangedAtUtc { get; set; }

    [IgnoreParse] public ICollection<DbFeedbackImage> Images { get; set; } = new HashSet<DbFeedbackImage>();
  }

  public class DbFeedbackConfiguration : IEntityTypeConfiguration<DbFeedback>
  {
    public void Configure(EntityTypeBuilder<DbFeedback> builder)
    {
      builder.ToTable(DbFeedback.TableName);

      builder.HasKey(f => f.Id);

      builder.HasMany(f => f.Images)
        .WithOne(fi => fi.Feedback)
        .HasForeignKey(fi => fi.FeedbackId);
    }
  }
}
