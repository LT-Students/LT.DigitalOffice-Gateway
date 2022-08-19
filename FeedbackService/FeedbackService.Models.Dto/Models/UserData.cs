using System;

namespace LT.DigitalOffice.FeedbackService.Models.Dto.Models
{
  public class UserData
  {
    public Guid Id { get; }

    public Guid? ImageId { get; }

    public string FirstName { get; }

    public string MiddleName { get; }

    public string LastName { get; }

    public bool IsActive { get; }
  }
}
