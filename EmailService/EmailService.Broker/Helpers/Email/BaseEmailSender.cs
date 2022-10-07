using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Data.Interfaces;
using LT.DigitalOffice.EmailService.Models.Db;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using Microsoft.Extensions.Logging;
using EASendMail;
using SystemSmtpClient = System.Net.Mail.SmtpClient;
using EASendMailSmtpClient = EASendMail.SmtpClient;

namespace LT.DigitalOffice.EmailService.Broker.Helpers
{
  public abstract class BaseEmailSender
  {
    private readonly ISmtpSettingsRepository _repository;
    protected readonly ILogger _logger;

    private async Task<SmtpServer> GetSmtpCredentialsAsync()
    {
      DbModuleSetting result = await _repository.GetAsync();

      SmtpServer oServer = null;

      if (result != null)
      {
        oServer = new SmtpServer(result.Host);
        oServer.ConnectType = SmtpConnectType.ConnectSTARTTLS;
        oServer.Port = result.Port;
        oServer.AuthType = SmtpAuthType.XOAUTH2;
        oServer.User = result.Email;
        oServer.Password = result.Password;

        return oServer;
      }

      _logger?.LogError("Cannot get smtp credentials.");

      return oServer;
    }

    protected async Task<bool> SendAsync(DbEmail dbEmail)
    {
      SmtpServer oServer = await GetSmtpCredentialsAsync();

      if (oServer is null)
      {
        return false;
      }

      try
      {
        SmtpMail oMail = new SmtpMail("TryIt");
        oMail.From = oServer.User;
        oMail.To = dbEmail.Receiver;
        oMail.Subject = dbEmail.Subject;
        oMail.TextBody = dbEmail.Text;

        EASendMailSmtpClient oSmtp = new EASendMailSmtpClient();
        oSmtp.SendMail(oServer, oMail);
      }
      catch (Exception exc)
      {
        _logger?.LogError(exc,
          "Errors while sending email with id {emailId} to {to}. Email replaced to resend queue.",
          dbEmail.Id,
          dbEmail.Receiver);

        return false;
      }
      return true;
    }

    protected async Task<bool> SendWithSmtpAsync(DbEmail dbEmail, SmtpInfo smtpInfo)
    {
      try
      {
        MailMessage message = new MailMessage(
        smtpInfo.Email,
        dbEmail.Receiver)
        {
          Subject = dbEmail.Subject,
          Body = dbEmail.Text
        };

        message.IsBodyHtml = true;

        SystemSmtpClient smtp = new SystemSmtpClient(
          smtpInfo.Host,
          smtpInfo.Port)
        {
          Credentials = new NetworkCredential(
            smtpInfo.Email,
            smtpInfo.Password),
          EnableSsl = smtpInfo.EnableSsl
        };

        smtp.Send(message);
      }
      catch (Exception exc)
      {
        _logger?.LogError(exc,
          "Errors while sending email with id {emailId} to {to}. Email replaced to resend queue.",
          dbEmail.Id,
          dbEmail.Receiver);

        return false;
      }
      return true;
    }

    public BaseEmailSender(
      ISmtpSettingsRepository repository,
      ILogger logger)
    {
      _repository = repository;
      _logger = logger;
    }
  }
}
