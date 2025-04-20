using MimeKit;
using MailKit.Security;
using Application.Services;
using Domain.Common;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public sealed class EmailSender(ILogger<EmailSender> logger) : IEmailSender
{

    private static string senderName = "EMAIL_SENDER_NAME".FromEnvRequired();
    private static string senderAddress = "EMAIL_SENDER_ADDRESS".FromEnvRequired();
    private static string smtpServer = "EMAIL_SMTP_SERVER".FromEnvRequired();
    private static int port = int.Parse("EMAIL_PORT".FromEnvRequired());
    private static string username = "EMAIL_USERNAME".FromEnvRequired();
    private static string password = "EMAIL_PASSWORD".FromEnvRequired();
    public async Task SendEmailAsync(string email, string subject, string message,CancellationToken cancellationToken = default)
    {
    

        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(senderName, senderAddress));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart("plain") { Text = message };

        using var client = new MailKit.Net.Smtp.SmtpClient();

        //Todo:Remove while end deploy
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

        await client.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls , cancellationToken);
        await client.AuthenticateAsync(username, password , cancellationToken);
        try
        {
            await client.SendAsync(emailMessage,cancellationToken);

        }
        catch (Exception ex)
        {
            //TODO: addSerilog!
            logger.LogError(ex,"Error.While Sending Message ");
            throw new InvalidOperationException("Failed to send OTP email", ex);

        }
        finally
        {
            await client.DisconnectAsync(true,cancellationToken);
        }
    }
}