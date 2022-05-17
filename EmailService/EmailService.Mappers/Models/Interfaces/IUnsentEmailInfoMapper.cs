﻿using LT.DigitalOffice.EmailService.Models.Db;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.EmailService.Mappers.Models.Interfaces
{
  [AutoInject]
  public interface IUnsentEmailInfoMapper
  {
    UnsentEmailInfo Map(DbUnsentEmail dbUnsentEmail);
  }
}
