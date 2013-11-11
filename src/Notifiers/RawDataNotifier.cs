using System;
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
            _sendNotificationRequest.Method = "POST";

            var rawMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<root>" +
                "<Value1>" + message + "<Value1>" +
            "</root>";

            var notificationMessage = Encoding.Default.GetBytes(rawMessage);

            _sendNotificationRequest.ContentLength = notificationMessage.Length;
            _sendNotificationRequest.ContentType = "text/xml";
            _sendNotificationRequest.Headers.Add("X-NotificationClass", "3");


            using (Stream requestStream = _sendNotificationRequest.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)_sendNotificationRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            var notificationStatus = response.Headers["X-NotificationStatus"];
            var notificationChannelStatus = response.Headers["X-SubscriptionStatus"];
            var deviceConnectionStatus = response.Headers["X-DeviceConnectionStatus"];

            Console.WriteLine(notificationStatus + " | " + deviceConnectionStatus + " | " + notificationChannelStatus);

        }


    }
}
