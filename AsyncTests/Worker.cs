using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncTests
{
    public class Worker
    {
        public void StartWorking()
        {
            while (Console.KeyAvailable == false)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString());                
                DoWork(); // method will be called synchronously - it will block
            }
        }

        public async void DoWork()
        {            
            //Task<int> computation = Task.Run(() => Compute(5));   <--- runs on separate thread
            //string result = DoDownload().Result;   <--- blocking

            Task<string> download = DoDownload();
            // await *INSIDE* the DoDownload method will yield control back here

            Console.WriteLine("Doing other work");
            Thread.Sleep(1000);
            Console.WriteLine("Other work done");

                
            string result = await download; // warning - might have yielded control to Program.cs
            Console.WriteLine("* Downloaded " + result.Length + " characters.");

        }

        public async Task<string> DoDownload()
        {
            Console.WriteLine("     Preparing to start downloading - did not yield control yet");
            var client = new WebClient();
            Thread.Sleep(1000);
            Console.WriteLine("     Started downloading - yielding control back to calling method");            
            string html = await client.DownloadStringTaskAsync("http://www.onet.pl");
            Console.WriteLine("     Finished downloading");
            return html;
        }
        

//        public async Task<int> Compute(int number)
//        {
//            Console.WriteLine("* Computing started");
//            Thread.Sleep(number * 1000);
//            Console.WriteLine("* Computing finished");
//            return number*2;
//        }
        
    }
}
