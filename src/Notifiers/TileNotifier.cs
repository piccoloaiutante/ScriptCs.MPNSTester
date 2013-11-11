//using System;

using System;
using System.IO;
using System.Net;
using System.Text;

namespace ScriptCs.MpnsTester.Notifiers
{
    public class TileNotifier
    {
        private readonly string _url;
        private HttpWebRequest _sendNotificationRequest;

        public TileNotifier(string url)
        {
            _url = url;
            _sendNotificationRequest = (HttpWebRequest)WebRequest.Create(_url);
        }

        public void Notify(string backgroundImage, string count, string title,
            string backBackgroundImage, string backTitle, string backContent)
        {

            _sendNotificationRequest = (HttpWebRequest)WebRequest.Create(_url);

            _sendNotificationRequest.Method = "POST";

            var toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                                  "<wp:Notification xmlns:wp=\"WPNotification\">" +

                                  "<wp:Tile>" +
                                  "<wp:BackgroundImage>" + backgroundImage + "</wp:BackgroundImage>" +
                                  "<wp:Count>" + count + "</wp:Count>" +
                                    "<wp:Title>" + title + "</wp:Title>" +
                                    "<wp:BackBackgroundImage>" + backBackgroundImage + "</wp:BackBackgroundImage>" +
                                    "<wp:BackTitle>" + backTitle + "</wp:BackTitle>" +
                                    "<wp:BackContent>" + backContent + "</wp:BackContent>" +
                                  "</wp:Tile> " +
                                  "</wp:Notification>";

            var notificationMessage = Encoding.Default.GetBytes(toastMessage);


            _sendNotificationRequest.ContentLength = notificationMessage.Length;
            _sendNotificationRequest.ContentType = "text/xml";
            _sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "token");
            _sendNotificationRequest.Headers.Add("X-NotificationClass", "1");

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
