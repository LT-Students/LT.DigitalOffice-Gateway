using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Data.Interfaces;
using LT.DigitalOffice.EmailService.Models.Db;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using Microsoft.Extensions.Logging;
using EASendMail;
using MimeKit;
using SystemSmtpClient = System.Net.Mail.SmtpClient;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;

namespace LT.DigitalOffice.EmailService.Broker.Helpers
{
  public abstract class BaseEmailSender
  {
    private readonly ISmtpSettingsRepository _repository;
    protected readonly ILogger _logger;

    private async Task<DbModuleSetting> GetSmtpCredentialsAsync()
    {
      DbModuleSetting result = await _repository.GetAsync();

      if (result is null)
      {
        _logger?.LogError("Cannot get smtp credentials.");
      }

      return result;
    }

    protected async Task<bool> SendAsync(DbEmail dbEmail)
    {
      DbModuleSetting result = await GetSmtpCredentialsAsync();

      if (result is null)
      {
        return false;
      }

      try
      {
        using SmtpClient client = new SmtpClient();
        await client.ConnectAsync(result.Host, result.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(result.Email, result.Password);

        MailboxAddress addressTo = new MailboxAddress("addressTo", dbEmail.Receiver);
        MailboxAddress addressFrom = new MailboxAddress("addressFrom", result.Email);

        BodyBuilder bodyBuilder = new BodyBuilder
        {
          TextBody = dbEmail.Text
        };

        MimeMessage emailMessage = new MimeMessage();
        emailMessage.From.Add(addressFrom);
        emailMessage.To.Add(addressTo);
        emailMessage.Subject = dbEmail.Subject;
        emailMessage.Body = bodyBuilder.ToMessageBody();

        await client.SendAsync(emailMessage);
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
