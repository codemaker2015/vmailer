using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class SendMailDemo : MonoBehaviour {

    string From = "codemaker2015@gmail.com";
    string Name = "Vishnu Sivan";
    public InputField To;
    public InputField Subject;
    public InputField Message;
    
    public string AttachmentFilename = "logo.jpg";
    
    public void SendMailWithAttachment()
    {
        MailSingleton.Instance.SendMailWithAttachment(
            From, 
            Name, 
            To.text, 
            Subject.text, 
            Message.text, 
            Application.streamingAssetsPath + "/" + AttachmentFilename
        );
    }

    public void SendPlainMail()
    {
        MailSingleton.Instance.SendPlainMail(
            From, 
            Name, 
            To.text, 
            Subject.text, 
            Message.text
        );
    }

    public void Quit() {
        Application.Quit();
    }

}
