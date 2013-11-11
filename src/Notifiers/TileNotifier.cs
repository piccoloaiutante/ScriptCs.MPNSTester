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

            // Create an HTTPWebRequest that posts the toast notification to the Microsoft Push Notification Service.
            // HTTP POST is the only method allowed to send the notification.
            _sendNotificationRequest.Method = "POST";

            // The optional custom header X-MessageID uniquely identifies a notification message. 
            // If it is present, the same value is returned in the notification response. It must be a string that contains a UUID.
            // sendNotificationRequest.Headers.Add("X-MessageID", "<UUID>");

            // Create the toast message.
            string toastMessage = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
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



            // Set the notification payload to send.
            byte[] notificationMessage = Encoding.Default.GetBytes(toastMessage);

            // Set the web request content length.
            _sendNotificationRequest.ContentLength = notificationMessage.Length;
            _sendNotificationRequest.ContentType = "text/xml";
            //sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "toast");
            //sendNotificationRequest.Headers.Add("X-NotificationClass", "2");
            _sendNotificationRequest.Headers.Add("X-WindowsPhone-Target", "token");
            _sendNotificationRequest.Headers.Add("X-NotificationClass", "1");

            using (Stream requestStream = _sendNotificationRequest.GetRequestStream())
            {
                requestStream.Write(notificationMessage, 0, notificationMessage.Length);
            }

            // Send the notification and get the response.
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)_sendNotificationRequest.GetResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            string notificationStatus = response.Headers["X-NotificationStatus"];
            string notificationChannelStatus = response.Headers["X-SubscriptionStatus"];
            string deviceConnectionStatus = response.Headers["X-DeviceConnectionStatus"];

            // Display the response from the Microsoft Push Notification Service.  
            // Normally, error handling code would be here. In the real world, because data connections are not always available,
            // notifications may need to be throttled back if the device cannot be reached.
            Console.WriteLine(notificationStatus + " | " + deviceConnectionStatus + " | " + notificationChannelStatus);
        }
    }
}
