using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LT.DigitalOffice.EmailService.Data.Interfaces;
using LT.DigitalOffice.EmailService.Models.Db;
using LT.DigitalOffice.EmailService.Models.Dto.Models;
using Microsoft.Extensions.Logging;
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

        MailboxAddress addressTo = new MailboxAddress(dbEmail.Receiver, dbEmail.Receiver);
        MailboxAddress addressFrom = new MailboxAddress(result.Email, result.Email);

        BodyBuilder bodyBuilder = new BodyBuilder
        {
          HtmlBody = string.Format("<p style='color:black;'>{0}</p>", dbEmail.Text) 
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
        using SmtpClient client = new SmtpClient();
        await client.ConnectAsync(smtpInfo.Host, smtpInfo.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(smtpInfo.Email, smtpInfo.Password);

        MailboxAddress addressTo = new MailboxAddress(dbEmail.Receiver, dbEmail.Receiver);
        MailboxAddress addressFrom = new MailboxAddress(smtpInfo.Email, smtpInfo.Email);

        BodyBuilder bodyBuilder = new BodyBuilder
        {
          HtmlBody = string.Format("<p style='color:black;'>{0}</p>", dbEmail.Text)
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

    public BaseEmailSender(
      ISmtpSettingsRepository repository,
      ILogger logger)
    {
      _repository = repository;
      _logger = logger;
    }
  }
}
