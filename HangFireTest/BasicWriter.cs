using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangFireTest
{
    class BasicWriter : IWriter
    {
        public void Write(string msg)
        {
            Thread.Sleep(1000);
            Console.WriteLine(msg);
        }
    }
}
