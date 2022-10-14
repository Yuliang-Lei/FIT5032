using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Assignment2.Models;

namespace FIT5032_Week08A.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "ENTER YOUR API HERE";
        public void Notification(String toEmail)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("lyl13974846792@gmail.com", "First Choice Pharmacy");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = "Welcome to First Choice Pharmacy";
            var htmlContent = "<p> Welcome to First Choice Pharmacy </p>";
            var msg = MailHelper.CreateSingleEmail(from, to, "no reply", plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("lyl13974846792@gmail.com", "First Choice Pharmacy");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p> Thanks for your enquiry, we'll send you feedback within the next few days! </p>";
            var msg = MailHelper.CreateSingleEmail(from, to, "no reply", plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void SendWithAttachment(String toEmail, String subject, String contents, String attachment)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("lyl13974846792@gmail.com", "First Choice Pharmacy");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p> Thanks for your enquiry, we'll send you feedback within the next few days! </p>" + plainTextContent;

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            string Path = HttpContext.Current.Server.MapPath("\\Uploads\\");
            var bytes = File.ReadAllBytes(Path + attachment);
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment(attachment, file);




            var response = client.SendEmailAsync(msg);
        }

    }
}