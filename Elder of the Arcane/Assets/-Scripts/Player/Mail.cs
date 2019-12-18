using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Mail;

public class Mail : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }
    public static void SendMail(){
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        mail.From = new MailAddress("Ps20ndean@efcts.us");
        mail.To.Add("Ps20jklausman@efcts.us");
        mail.Subject = "Test Mail 25";
        mail.Body = "Test does you work";

        //System.Net.Mail.Attachment attachment;
        //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
        //mail.Attachments.Add(attachment);


        SmtpServer.Port = 25;
        SmtpServer.Credentials = new System.Net.NetworkCredential("Ps20ndean@efcts.us", "Gracie11");
        SmtpServer.EnableSsl = true;

        SmtpServer.Send(mail);
    }
}
