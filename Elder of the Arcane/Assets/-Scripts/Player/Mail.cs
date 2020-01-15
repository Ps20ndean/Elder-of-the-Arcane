using System.IO;
using UnityEngine.Networking;
using UnityEngine;
using System.Net.Mail;

public class Mail : MonoBehaviour
{
    public GameObject reporting;
    private UserReportingScript actualEmail;
    private UnityWebRequest web;
    private bool IsSending= false;

    private void Awake()
    {
        actualEmail = reporting.GetComponent<UserReportingScript>();
    }
    
        public void SendMail()
        {
             if (IsSending)
             {
                   // runs a check to see if the email is already trying to send 
                   Debug.LogWarning("Tried to send mail, when the SMTPClient was sending mail.");
                   return;
             }

             IsSending = true;


   
             try
             {
                 //sends an email to the user using the log file. the try/catch catches if a non-email address is entered
                 MailAddress email = new MailAddress(actualEmail.email_text.text);
                 WWWForm form = new WWWForm();
                 form.AddField("address", email.Address);
                 form.AddField("subject", "Crash Report");
                 form.AddField("email_body", System.IO.File.ReadAllText("Logs/Log.txt"));
                 web = UnityWebRequest.Post("http://ts.jaytechmedia.com:4096/smtp/send", form);
                 UnityWebRequestAsyncOperation request = web.SendWebRequest();
                 request.completed += SendFinished;
             }
            catch
             {
                 //catches a wrong email address
                 IsSending = false;
                 Debug.LogWarning("Not an actual email address.");
                 return;
             }
        }

    private void SendFinished(AsyncOperation operation)
    {
        if(web.responseCode == 200)
        {
            Debug.Log("Mail Sent Successfully!");
        }
        else
        {
            Debug.LogWarning("SendMail Error, Server Returned : " + web.responseCode + " : " + web.ToString());
        }
        IsSending = false;
    }

}
