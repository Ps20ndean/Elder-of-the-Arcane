using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using Microsoft.VisualBasic;

public class Mail : MonoBehaviour
{
 
    void Update()
    {
        
    }
    public static void SendMail(){
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        mail.From = new MailAddress("EOTAError@gmail.com");
        string email = UserReportingScript.email;
        mail.To.Add(email);
        mail.Subject = "Crash Report";
        mail.Body = CrashReport.lastReport.ToString();
        SmtpServer.Port = 25;
        SmtpServer.Credentials = new System.Net.NetworkCredential("EOTAError@gmail.com", "EOTA824019");
        SmtpServer.EnableSsl = true;
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            
        }
        catch
        {
            return;
        }
        SmtpServer.Send(mail);
       

    }
}
