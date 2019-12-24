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

        private static System.Timers.Timer aTimer;
        private static System.Timers.Timer bTimer;
        protected override void OnStart(string[] args)
        {
            aTimer = new System.Timers.Timer(5000);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Enabled = true;

            bTimer = new System.Timers.Timer(600000);
            bTimer.Elapsed += new ElapsedEventHandler(OnDeleteEvent);
            bTimer.Enabled = true;
        }

        private static void OnDeleteEvent(object source, ElapsedEventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "CurrentTime.txt";

            if (!File.Exists(path))
            {

            }
            else if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            string currentDateAndTime = DateTime.Now.ToString("h:mm:ss tt");
            string filePath = (AppDomain.CurrentDomain.BaseDirectory + "CurrentTime.txt");

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                    sw.WriteLine("The current time in Orlando is: " + currentDateAndTime);
                }
                else if (File.Exists(filePath))
                {
                    sw.WriteLine("The current time in Orlando is: " + currentDateAndTime);
                }
                sw.Close();
            }
        }

        protected override void OnStop()
        {
            string endTime = DateTime.Now.ToString("h:mm:ss tt");
            string filePath = (AppDomain.CurrentDomain.BaseDirectory + "ServiceEnded.txt");

            using (StreamWriter endWriter = new StreamWriter(filePath, true))
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                    endWriter.WriteLine("Service Ended At: " + endTime);
                }
                else if (File.Exists(filePath))
                {
                    endWriter.WriteLine("Service Ended At: " + endTime);
                }
            }
        }
    }
}
