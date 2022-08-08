using LT.DigitalOffice.FeedbackService.Data.Interfaces;
using LT.DigitalOffice.FeedbackService.Data.Provider;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.FeedbackService.Data
{
  public class FeedbackRepository : IFeedbackRepository
  {
    private readonly IDataProvider _provider;

    #region private methods

    private IQueryable<DbFeedback> CreateFindPredicate(FindFeedbacksFilter filter)
    {
      IQueryable<DbFeedback> query = _provider.Feedbacks.AsQueryable();

      if (filter.FeedbackType.HasValue)
      {
        query = query.Where(f => f.FeedbackType == (int)filter.FeedbackType);
      }

      if (filter.FeedbackStatus.HasValue)
      {
        query = query.Where(f => f.Status == (int)filter.FeedbackStatus);
      }

      return query;
    }

    #endregion

    public FeedbackRepository(IDataProvider provider)
    {
      _provider = provider;
    }

    public async Task<(List<DbFeedback> dbFeedbacks, int totalCount)> FindAsync(FindFeedbacksFilter filter)
    {
      if (filter is null)
      {
        return (null, 0);
      }

      IQueryable<DbFeedback> query = CreateFindPredicate(filter);

      int totalCount = await query.CountAsync();

      var dbFeedbacks = await query
        .Skip(filter.SkipCount)
        .Take(filter.TakeCount)
        .OrderByDescending(f => f.CreatedAtUtc)
        .ToListAsync();

      return (dbFeedbacks, totalCount);
    }

    public async Task<Guid?> CreateAsync(DbFeedback dbFeedback)
    {
      if (dbFeedback is null)
      {
        return null;
      }

      _provider.Feedbacks.Add(dbFeedback);
      await _provider.SaveAsync();

      return dbFeedback.Id;
    }
  }
}
