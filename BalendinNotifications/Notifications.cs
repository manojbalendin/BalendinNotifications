using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace BalendinNotifications
{
    public class Notifications
    {
        private string FromEmail { get; set; }
        private MailAddressCollection ToAddress { get; set; }
        private MailAddressCollection CCAddress { get; set; }
        private MailAddressCollection BCCAddress { get; set; }
        private string Subject { get; set; }
        private string Body { get; set; }
        private int HostPort { get; set; }
        private string HostServer { get; set; }
        private string CredentialUserName { get; set; }
        private string CredentialUserPassword{get;set;}
        public Notifications(string fromEmail, MailAddressCollection toAddress, MailAddressCollection ccAddress, MailAddressCollection bccAddress, string subject, string body,
            int hostPort, string hostServer, string credentialUserName, string credentialUserPassword)
        {
            this.FromEmail = fromEmail;
            this.ToAddress = ToAddress;
            this.CCAddress = ccAddress;
            this.BCCAddress = bccAddress;
            this.Subject = subject;
            this.Body = body;
            this.HostPort = hostPort;
            this.HostServer = hostServer;
            this.CredentialUserName = credentialUserName;
            this.CredentialUserPassword = credentialUserPassword;
        }

        public int Send()
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(FromEmail);
                foreach (var toaddress in ToAddress)
                {
                    message.To.Add(toaddress);
                }
                foreach (var ccaddress in CCAddress)
                {
                    message.CC.Add(ccaddress);
                }
                foreach (var bccaddress in BCCAddress)
                {
                    message.Bcc.Add(bccaddress);
                }
                message.Subject = this.Subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = this.Body;
                smtp.Port = this.HostPort;
                smtp.Host = this.HostServer;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(this.CredentialUserName, this.CredentialUserPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch(Exception ex)
            {
                return 0;
            }
            return 1;
        }
    }
}
