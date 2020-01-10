using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using Microsoft.VisualBasic;

public class Mail : MonoBehaviour
{
    public GameObject reporting;
    void Update()
    {
        
    }
    public void SendMail(){
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        mail.From = new MailAddress("EOTAError@gmail.com");
        Debug.Log("Reporting : " + reporting);
        var actualEmail = reporting.GetComponent<UserReportingScript>();
        Debug.Log("actualEmail : " + actualEmail);
        string emaill = actualEmail.email;
        mail.To.Add(emaill);
        mail.Subject = "Crash Report";
        mail.Body = CrashReport.lastReport.ToString();
        SmtpServer.Port = 25;
        SmtpServer.Credentials = new System.Net.NetworkCredential("EOTAError@gmail.com", "EOTA08242019");
        SmtpServer.EnableSsl = true;
        try
        {
            var addr = new MailAddress(emaill);
            
        }
        catch
        {
            return;
        }
        SmtpServer.Send(mail);
       

    }
}
