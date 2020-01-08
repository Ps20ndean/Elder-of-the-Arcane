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
        
        mail.To.Add("Ps20jklausman@efcts.us");
        mail.Subject = "Crash Report";
        mail.Body = CrashReport.lastReport.ToString();
        SmtpServer.Port = 25;
        SmtpServer.Credentials = new System.Net.NetworkCredential("EOTAError@gmail.com", "EOTA824019");
        SmtpServer.EnableSsl = true;
       

        SmtpServer.Send(mail);
    }
}
