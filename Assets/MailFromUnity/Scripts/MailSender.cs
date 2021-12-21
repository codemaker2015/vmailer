using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ESN
{
    public class MailSender
    {

        private SmtpClient mSmtpServer;

        public MailSender(string username, string password, string smtpServerAddress, int port = 25, bool enableSSL = false, bool useDefaultCredentials = false)
        {
            
            mSmtpServer = new SmtpClient(smtpServerAddress);
            mSmtpServer.EnableSsl = enableSSL;
            mSmtpServer.UseDefaultCredentials = useDefaultCredentials;
            mSmtpServer.Port = port;
            mSmtpServer.Credentials = new System.Net.NetworkCredential(username, password) as ICredentialsByHost;

            // A trick to enbale SSL with Google, I didn't tried other servers
            if (enableSSL)
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    { return true; };
            }
        }

        public void SendMail(string from, string fromName, string to, string subject, string body, SendCompletedEventHandler onComplete = null, string attachmentName = "", string attachmentKind = "image/jpg")
        {
            MailMessage mail = new MailMessage();
            
            mail.From = new MailAddress(from, fromName);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;

            if (attachmentName != "")
            {
                mail.Attachments.Add(new Attachment(attachmentName, attachmentKind));
            }

            if (onComplete != null)
            {
                mSmtpServer.SendCompleted += onComplete;
            }

            mSmtpServer.SendAsync(mail, null);
        }
    }
}
