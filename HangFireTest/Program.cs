using System;
using System.IO;
using System.Threading;
using Hangfire;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using Microsoft.Owin.Hosting;

namespace HangFireTest
{
    class Program
    {
        private static int counter;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));

            const string endpoint = "http://localhost:12345";

            //LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());

            using (WebApp.Start<Startup>(endpoint))
            {
                var id1 = BackgroundJob.Enqueue<IWriter>(w => w.Write("first"));
                var id2 = BackgroundJob.ContinueWith<IWriter>(id1, w => w.Write("second"));

                Console.ReadLine();
            }
        }
    }
}
