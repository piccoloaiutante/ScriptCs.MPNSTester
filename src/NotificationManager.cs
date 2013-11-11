using ScriptCs.Contracts;
using ScriptCs.MpnsTester.Notifiers;


namespace ScriptCs.MpnsTester
{
    public class NotificationManager:IScriptPackContext
    {
       public void RawDataSend(string message,string url)
        {
            var rawDataNotifier = new RawDataNotifier(url);
            rawDataNotifier.Notify(message);
        }

        public void TileNotificationSend(string backgroundImage, string count, string title,
            string backBackgroundImage, string backTitle, string backContent,string url)
        {
            var tileNotifier = new TileNotifier(url);
            tileNotifier.Notify(backgroundImage,count,  title,backBackgroundImage,  backTitle,  backContent);
        }

        public void ToastNotificationSend(string title, string content,string url)
        {
            var toastNotifier = new ToastNotifier(url);
            toastNotifier.Notify(title,content);
        }
    }
}
