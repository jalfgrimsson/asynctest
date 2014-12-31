using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTests
{
    public class AnotherWorker
    {
        public void StartWorking()
        {
            while (Console.KeyAvailable == false)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString());

                DoWork(); // method will be called synchronously - it will block
                          // control will be returned immediately because line 2 in DoWork() has await
                Console.WriteLine("Doing other work");
                Thread.Sleep(1000); // this other work will be performed
            }
        }

        public async void DoWork()
        {
            Console.WriteLine("     Start computation");
            int result = await Task.Run(() => Compute(5)); // <-- this will be launched in other thread
                                                           // control will be returned to calling method 
            Console.WriteLine("     Computed value " + result); // after 5 seconds, this will resume
        }

        public int Compute(int number)
        {
            Thread.Sleep(number * 1000);
            return number*2;
        }
    }
}
