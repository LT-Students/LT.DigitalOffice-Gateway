using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.EFSupport.Provider;
using LT.DigitalOffice.Kernel.Enums;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.FeedbackService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    DbSet<DbFeedback> Feedbacks { get; set; }
    DbSet<DbFeedbackImage> FeedbacksImages { get; set; }
  }
}
