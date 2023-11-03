using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace EmailNotification.Helper
{
    public class EmailSender: IEmailSender
    {
        private readonly MailJetSettings _mailJetSettings;

        public EmailSender(IOptions<MailJetSettings> mailjetSettings)
        {
            _mailJetSettings = mailjetSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient(_mailJetSettings.PublicKey,
                _mailJetSettings.PrivateKey);
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.FromEmail, _mailJetSettings.Email)
               .Property(Send.FromName, "Mailjet Pilot")
               .Property(Send.Subject, subject)
               .Property(Send.HtmlPart, htmlMessage)
               .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", email}
                 }
                   });
            MailjetResponse response = await client.PostAsync(request);
        }
    }
}
