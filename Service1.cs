using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Timers;

namespace CurrentTimeWindowsService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        public void OnDeBug()
        {
            OnStart(null);
        }

        private static System.Timers.Timer aTimer;
        protected override void OnStart(string[] args)
        {
            aTimer = new System.Timers.Timer(5000);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "CurrentTime.txt";
            string currentDateAndTime = DateTime.Now.ToString("h:mm:ss tt");

            if (!File.Exists(path))
            {
                File.Create(path);
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine("The current time in Orlando is: " + currentDateAndTime);
                tw.Close();
            }
            else if (File.Exists(path))
            {
                TextWriter tw = new StreamWriter(path, true);
                tw.WriteLine("The current time in Orlando is: " + currentDateAndTime);
                tw.Close();
            }
        }

        protected override void OnStop()
        {
            string endServicePath = AppDomain.CurrentDomain.BaseDirectory + "ServiceEnded.txt";
            string endServiceDateAndTime = DateTime.Now.ToString("h:mm:ss tt");

            if (!File.Exists(endServicePath))
            {
                File.Create(endServicePath);
                TextWriter tw = new StreamWriter(endServicePath);
                tw.WriteLine("Service ended at " + endServiceDateAndTime);
                tw.Close();
            }
            else if (File.Exists(endServicePath))
            {
                TextWriter tw = new StreamWriter(endServicePath, true);
                tw.WriteLine("Service ended at " + endServiceDateAndTime);
                tw.Close();
            }
        }
    }
}
