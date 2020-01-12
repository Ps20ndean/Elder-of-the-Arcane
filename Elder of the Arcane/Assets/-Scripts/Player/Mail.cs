using System.IO;
using UnityEngine.Networking;
using UnityEngine;
using System.Net.Mail;

public class Mail : MonoBehaviour
{
    public GameObject reporting;
    private UserReportingScript actualEmail;
    private UnityWebRequest web;
    private bool IsSending;

    private void Awake()
    {
        actualEmail = reporting.GetComponent<UserReportingScript>();
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
            MailAddress email = new MailAddress(actualEmail.email_text.text);
            //string path = "Logs/Log.txt";
            //StreamReader reader = new StreamReader(path);
            //web.SetRequestHeader("body", reader.ReadToEnd());
            //reader.Close();
            WWWForm form = new WWWForm();
            form.AddField("address", email.Address);
            form.AddField("subject", "Crash Report");
            form.AddField("email_body", "Test Email");

            web = UnityWebRequest.Post("http://ts.jaytechmedia.com:4096/smtp/send", form);
            UnityWebRequestAsyncOperation request = web.SendWebRequest();
            request.completed += SendFinished;
        }
        catch
        {
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
