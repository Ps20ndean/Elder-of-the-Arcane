using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using UnityEngine;

public class Mail : MonoBehaviour
{
    public GameObject reporting;
    private UserReportingScript actualEmail;
    private SmtpClient SmtpServer;
    private MailMessage mail = new MailMessage();
    private bool IsSending;

    private void Awake()
    {
        actualEmail = reporting.GetComponent<UserReportingScript>();

        SmtpServer = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("eota.error@gmail.com", "EOTA08242019"),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
        };

        SmtpServer.SendCompleted += SendFinished;
    }

    private void OnDisable()
    {
        SmtpServer.SendCompleted -= SendFinished;
        SmtpServer.Dispose();
    }

    public void SendMail()
    {
        if (IsSending)
        {
            Debug.LogWarning("Tried to send mail, when the SMTPClient was sending mail.");
            return;
        }

        IsSending = true;


       
        try
        {
            string email = actualEmail.email_text.text;
            MailAddress addr = new MailAddress(email);
            mail.To.Clear();

            string path = "Logs/Log.txt";
            StreamReader reader = new StreamReader(path);

            mail.From = new MailAddress("Elder of The Arcane <eota.error@gmail.com>");
            mail.To.Add(addr);
            mail.Subject = "Crash Report";
            //mail.Body = reader.ReadToEnd();
            mail.Body = "Test Email";
            reader.Close();
        }
        catch
        {
            IsSending = false;
            Debug.LogWarning("Not an actual email address.");
            return;
        }
        SmtpServer.SendMailAsync(mail);
    }

    private void SendFinished(object sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled)
        {
            Debug.LogWarning("Cancelled Sending Email.");
        }
        else if (e.Error != null)
        {
            Debug.LogWarning("Error Sending Email : " + e.Error.Message);
            Debug.LogWarning(e.Error.ToString());
        }
        else
        {
            Debug.Log("Email sent!");
        }

        IsSending = false;
    }

}
