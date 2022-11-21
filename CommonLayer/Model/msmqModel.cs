using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class msmqModel
    {
        MessageQueue messageQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            messageQ.Path = @".\private$\token";

            if(!MessageQueue.Exists(messageQ.Path))
            {
                MessageQueue.Create(messageQ.Path);
                //Exists
            }
            

            messageQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQ.ReceiveCompleted += MessageQ_ReceiveCompleted;
            messageQ.Send(token);
            messageQ.BeginReceive();
            messageQ.Close();
        }

        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = messageQ.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            string subject = "Fundoo Notes reset link";
            string body = token;
            var SMTP = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("aarvikkalburgi@gmail.com", "qhjioxlwphdzfiwp"),
                EnableSsl = true,
            };
            SMTP.Send("aarvikkalburgi@gmail.com", "aarvikkalburgi@gmail.com", subject, body);
        }
    }
}
