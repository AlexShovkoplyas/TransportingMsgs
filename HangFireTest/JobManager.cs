using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFireTest
{
    class JobManager
    {
        private IWriter writer;

        public JobManager(IWriter writer)
        {
            this.writer = writer;
        }

        public void AssignJob(string msg)
        {
            writer.Write(msg);
        }

        internal void RunTestJobs()
        {
            var id = BackgroundJob.Enqueue(() => writer.Write("first"));
            var id2 = BackgroundJob.ContinueWith(id, () => writer.Write("second"));
            var id3 = BackgroundJob.ContinueWith(id2, () => writer.Write("third"));

            BackgroundJob.Schedule(() => writer.Write("scheduled"), TimeSpan.FromSeconds(2));
            
            Console.WriteLine("Waiting for jobs");
            Console.ReadLine();
        }
    }
}
