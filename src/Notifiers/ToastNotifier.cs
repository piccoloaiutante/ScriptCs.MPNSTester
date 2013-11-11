using System;
using System.IO;
using System.Text;
using System.Net;

namespace ScriptCs.MpnsTester.Notifiers
{
    internal class ToastNotifier
    {
        private readonly string _url;

        public ToastNotifier(string url)
        {
            _url = url;
        }

        public void Notify(string title,string content)
        {
            HttpWebRequest sendNotificationRequest = (HttpWebRequest)WebRequest.Create(_url);

            // Create an HTTPWebRequest that posts the toast notification to the Microsoft Push Notification Service.
            // HTTP POST is the only method allowed to send the notification.
            sendNotificationRequest.Method = "POST";


            var toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                           "<wp:Notification xmlns:wp=\"WPNotification\">" +
                           "<wp:Toast>" +
                           "<wp:Text1>" + title + "</wp:Text1>" +
                           "<wp:Text2>" + content + "</wp:Text2>" +
                           "<wp:Param>/Views/SyncView.xaml</wp:Param>" +
                           "</wp:Toast> " +
                           "</wp:Notification>";

            // Create an HTTPWebRequest that posts the toast notification to the Microsoft Push Notification Service.
            // HTTP POST is the only method allowed to send the notification.
            sendNotificationRequest.Method = "POST";

            var notificationMessage = Encoding.Default.GetBytes(toastMessage);

            // Set the web request content length.
            sendNotificationRequest.ContentLength = notificationMessage.Length;
            sendNotificationRequest.ContentType = "text/xml";
            sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "toast");
            sendNotificationRequest.Headers.Add("X-NotificationClass", "2");


            using (Stream requestStream = sendNotificationRequest.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }

            // Send the notification and get the response.
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse) sendNotificationRequest.GetResponse();
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
