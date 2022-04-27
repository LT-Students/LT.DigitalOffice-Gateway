﻿using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Database;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.EmailService.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace LT.DigitalOffice.EmailService.Data.Provider
{
  [AutoInject(InjectType.Scoped)]
  public interface IDataProvider : IBaseDataProvider
  {
    DbSet<DbEmail> Emails { get; set; }
    DbSet<DbUnsentEmail> UnsentEmails { get; set; }
    DbSet<DbModuleSetting> ModuleSettings { get; set; }
  }
}
