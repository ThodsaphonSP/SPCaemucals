using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using SPCaemucals.Data.Identities;

namespace SPCaemucals.Backend.Services;

public class EmailSender : IEmailSender<ApplicationUser>
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using (var client = new SmtpClient("smtp.gmail.com", 587))
        {
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("YourEmail", "YourPassword");
            client.EnableSsl = true;

            using (var emailMessage = new MailMessage())
            {
                emailMessage.From = new MailAddress("YourEmail");
                emailMessage.To.Add(new MailAddress(email));
                emailMessage.Subject = subject;
                emailMessage.Body = message;
                await client.SendMailAsync(emailMessage);
            }
        }
    }

    public async Task SendConfirmationLinkAsync(ApplicationUser user, string link, string callbackUrl)
    {
        string message = $"<a href='{HtmlEncoder.Default.Encode(link)}'>Click here</a> to confirm your account";
        if (user.Email != null) await SendEmailAsync(user.Email, "Confirmation Email", message);
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string code, string callbackUrl)
    {
        // Implementation of your logic to send a password reset code via email
        // ...

        return Task.CompletedTask;
    }

    public async Task SendPasswordResetLinkAsync(ApplicationUser user, string link, string callbackUrl)
    {
        string message = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(link)}'>clicking here</a>.";
        if (user.Email != null) await SendEmailAsync(user.Email, "Reset Password", message);
    }
}