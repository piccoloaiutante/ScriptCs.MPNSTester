using System.IO;
using System.Net;
using System.Text;

namespace ScriptCs.MpnsTester.Notifiers
{
    public class RawDataNotifier
    {
        private readonly string _url;
        private HttpWebRequest _sendNotificationRequest;

        public RawDataNotifier(string url)
        {
            _url = url;
            _sendNotificationRequest = (HttpWebRequest)WebRequest.Create(_url);
        }

        public void Notify(string message)
        {
           

            // Create an HTTPWebRequest that posts the raw notification to the Microsoft Push Notification Service.
            // HTTP POST is the only method allowed to send the notification.
            _sendNotificationRequest.Method = "POST";

            // Create the raw message.
            string rawMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<root>" +
                "<Value1>" + message+ "<Value1>" +
            "</root>";

            // Set the notification payload to send.
            byte[] notificationMessage = Encoding.Default.GetBytes(rawMessage);

            // Set the web request content length.
            _sendNotificationRequest.ContentLength = notificationMessage.Length;
            _sendNotificationRequest.ContentType = "text/xml";
            _sendNotificationRequest.Headers.Add("X-NotificationClass", "3");


            using (Stream requestStream = _sendNotificationRequest.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }

            // Send the notification and get the response.
            HttpWebResponse response = (HttpWebResponse)_sendNotificationRequest.GetResponse();

        }

       
    }
}
