using LT.DigitalOffice.FeedbackService.Mappers.Db.Interfaces;
using LT.DigitalOffice.FeedbackService.Models.Db;
using LT.DigitalOffice.FeedbackService.Models.Dto.Enums;
using LT.DigitalOffice.FeedbackService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LT.DigitalOffice.FeedbackService.Mappers.Db
{
  public class DbFeedbackMapper : IDbFeedbackMapper
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IDbFeedbackImageMapper _imageMapper;

    public DbFeedbackMapper(
      IHttpContextAccessor httpContextAccessor,
      IDbFeedbackImageMapper imageMapper)
    {
      _httpContextAccessor = httpContextAccessor;
      _imageMapper = imageMapper;
    }
    public DbFeedback Map(CreateFeedbackRequest request, List<Guid> imageIds)
    {
      if (request is null)
      {
        return null;
      }

      var feedbackId = Guid.NewGuid();

      return new DbFeedback
      {
        Id = feedbackId,
        FeedbackType = (int)request.Type,
        Content = request.Content,
        Status = (int)FeedbackStatusType.New,
        //TODO: Fill sender full name and ip
        SenderFullName = "",
        SenderId = _httpContextAccessor.HttpContext.GetUserId(),
        SenderIp = "",
        CreatedAtUtc = DateTime.Now,
        Images = imageIds?
          .Select(imageId => _imageMapper.Map(imageId, feedbackId))
          .ToList()
      };
    }
  }
}
