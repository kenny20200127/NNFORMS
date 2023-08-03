
using System.Net;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using NNPEFWEB.Models;
using System.IO;
using NNPEFWEB.Service;

namespace NNPEFWEB.Services
{
	public class EmailSenderService : IEmailSenderService
	{
		private readonly SmtpSettings _smtpSettings;
        private readonly MailMessage _mailMessage;

        public EmailSenderService(IOptionsSnapshot<SmtpSettings> smtpSettings,MailMessage mailMessage)
		{
			_smtpSettings = smtpSettings.Value;
            _mailMessage = mailMessage;

        }
		public string SendEmailAsync(EmailModel mailRequest)
		{
			var message = new MailMessage();
			message.From = (new MailAddress(_smtpSettings.SenderEmail));
			message.To.Add(new MailAddress(mailRequest.to));
			message.Subject = mailRequest.subject;

            if (mailRequest.html != null)
            {
                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.html;
                message.Body = builder.ToMessageBody().ToString();
                message.IsBodyHtml = true;
            }
            else
            {
                message.Body = string.Format(mailRequest.text);
                message.IsBodyHtml = false;
            }

            if (mailRequest.Attachments != null)
			{
				foreach (var file in mailRequest.Attachments)
				{
					if (file.Length > 0)
					{
						using (var ms = new MemoryStream())
						{
							file.CopyTo(ms);
							var fileBytes = ms.ToArray();
							Attachment att = new Attachment(new MemoryStream(fileBytes), file.FileName);
							message.Attachments.Add(att);
						}
					}
				}
			}
            ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
				   | SecurityProtocolType.Tls11
				   | SecurityProtocolType.Tls12;


            using(SmtpClient smtpClient=new SmtpClient())
            {

                
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = _smtpSettings.Server;
                smtpClient.Port = _smtpSettings.Port;
                smtpClient.Credentials = new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password);
                smtpClient.EnableSsl = _smtpSettings.SSL;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Timeout = 200000;
                smtpClient.Send(message);
                smtpClient.Dispose();
                return "Message Sent Successfully";
            }
			


		}
        public string SendMultipleEmail(EmailModel2 mailRequest)
        {
            var message = _mailMessage;
                message.From = new MailAddress(_smtpSettings.SenderEmail);
                string[] toId = mailRequest.to.Split(",");
                foreach (var tomail in toId)
                {
                   message.To.Add(new MailAddress(tomail));
                }
           
                if (mailRequest.cc!=null)
                {
                string[] ccId = mailRequest.to.Split(",");
                foreach (var ccmail in ccId)
                    {
                        message.CC.Add(new MailAddress(ccmail));
                    }
                }
                if (mailRequest.bcc != null)
                {
                string[] bccId = mailRequest.to.Split(",");
                foreach (var bccmail in bccId)
                    {
                        message.Bcc.Add(new MailAddress(bccmail));
                    }
                }
            
                message.Subject = mailRequest.subject;
                message.Body = string.Format(mailRequest.text);
                if (mailRequest.Attachments != null)
                {
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                Attachment att = new Attachment(new MemoryStream(fileBytes), file.FileName);
                                message.Attachments.Add(att);
                            }
                        }
                    }
                }
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;



                var smtp = new SmtpClient
                {
                    Host = _smtpSettings.Server,
                    Port = _smtpSettings.Port,
                    EnableSsl = _smtpSettings.SSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password)
                };
                smtp.Port = _smtpSettings.Port;
                smtp.Credentials = new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password);
                smtp.EnableSsl = _smtpSettings.SSL;
                smtp.Timeout = 200000;
                smtp.Send(message);
                smtp.Dispose();
			   return "Message Sent Successfully";

            

        }
       
    }
}
