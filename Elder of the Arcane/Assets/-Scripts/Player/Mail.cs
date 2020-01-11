using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;
using Microsoft.VisualBasic;
using System.IO;

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
        string emaill = actualEmail.email_text.text;
        mail.To.Add(emaill);
        mail.Subject = "Crash Report";
        string path = "Logs/Log.txt";
        //Read the text from directly from the Log.txt file
        StreamReader reader = new StreamReader(path);
        mail.Body = reader.ReadToEnd();
        reader.Close();
        SmtpServer.Port = 25;
        SmtpServer.Credentials = new System.Net.NetworkCredential("EOTA.Error@gmail.com", "EOTA08242019");
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
