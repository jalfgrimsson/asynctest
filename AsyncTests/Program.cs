using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //var worker = new Worker();
            var worker = new AnotherWorker();
            worker.StartWorking();
            //Console.ReadKey();
        }
    }
}
