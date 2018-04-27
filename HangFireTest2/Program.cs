using Hangfire;
using Hangfire.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFireTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("Connection1");

            using (var server = new BackgroundJobServer()) 
            {
                var id1 = BackgroundJob.Enqueue(() => Console.WriteLine("first"));
                var id2 = BackgroundJob.Enqueue(() => Console.WriteLine("second"));

                Console.ReadLine();
            }


        }
    }
}
