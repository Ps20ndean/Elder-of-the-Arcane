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
    private bool IsSending;

    private void Awake()
    {
        actualEmail = reporting.GetComponent<UserReportingScript>();

        SmtpServer = new SmtpClient("smtp.gmail.com")
        {
            Port = 465,
            Credentials = new NetworkCredential("EOTA.Error@gmail.com", "EOTA08242019"),
            EnableSsl = true
        };
    }


    public void SendMail()
    {
        if (IsSending)
        {
            Debug.LogWarning("Tried to send mail, when the SMTPClient was sending mail.");
            return;
        }

        IsSending = true;

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("EOTA.Error@gmail.com");
        string email = actualEmail.email_text.text;
        mail.To.Add(email);
        mail.Subject = "Crash Report";
        string path = "Logs/Log.txt";
        StreamReader reader = new StreamReader(path);
        //mail.Body = reader.ReadToEnd();
        mail.Body = "Test Email";
        reader.Close();
       
        try
        {
            var addr = new MailAddress(email);

        }
        catch
        {
            IsSending = false;
            Debug.LogWarning("Not an actual email address.");
            return;
        }
        SmtpServer.SendCompleted += SendFinished;
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
