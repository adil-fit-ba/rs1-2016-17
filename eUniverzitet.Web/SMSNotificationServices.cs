using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace eUniverzitet.Web
{
    public class SMSNotificationServices
    {
        private System.Timers.Timer _timerForNotifications;

        public SMSNotificationServices()
        {
            Start(10000);
        }


        /// <summary>
        ///  Metoda se poziva svakih N milisekudni koji je konfigurisan u web.config file-u.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timerForNotifications_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timerForNotifications.Enabled = false;


            // do nešto
            string createText = "Hello and Welcome" + Environment.NewLine;


            File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath("~") + "\\Ispis" + DateTime.Now.ToString("yyyy-MMMM-dd_HH-mm-ss.txt"), createText);


            _timerForNotifications.Enabled = true;
        }



        public void Start(double intervalForSendingNotifications)
        {
            _timerForNotifications = new System.Timers.Timer(intervalForSendingNotifications);
            _timerForNotifications.Interval = intervalForSendingNotifications;
            _timerForNotifications.Elapsed += _timerForNotifications_Elapsed;
            _timerForNotifications.Start();
        }

        public void Stop()
        {
            _timerForNotifications.Stop();
        }
    }
}