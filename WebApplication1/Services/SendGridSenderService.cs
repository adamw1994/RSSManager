using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSSreader.Services
{
    public class SendGridSenderService
    {
        public async Task<bool> SendEmail(string receiverEmail, string title, string content)
        {
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("azure_9216a8fc8d158c3cdbf36ea218f11ce1@azure.com"));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress("wybraniecadam@gmail.com"),
            };
            msg.AddTos(recipients);

            msg.SetSubject(title);
            msg.AddContent(MimeType.Html, content);

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var response = await client.SendEmailAsync(msg);
            var a = response.Body.ReadAsStringAsync();
            return true;
        }
    }
}
