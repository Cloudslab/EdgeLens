using System;
using System.Collections.Generic;
using System.Text;
using Aneka;
using Aneka.Threading;
using Aneka.Entity;
using System.Threading;
using System.IO;
using System.Net;

namespace AnekaThreadPractise1
{
    [Serializable]
    public class HelloWorld
    {
        public string masterIP;
        public HelloWorld(string IP)
        {
            this.masterIP = IP;
        }
        public void PrintHello()
        {
            string input = String.Concat("http://", masterIP, "/EdgeLens/Aneka/input.jpg");
            string output = String.Concat("http://", masterIP, "/EdgeLens/Aneka/0_0.jpg");
            using (var client = new WebClient())
            {
                Console.WriteLine("Sending input to worker");
                client.DownloadFile(input, "input.jpg");
            }
            string strCmdText = "mv input.jpg ../Yolo/test/images/input.jpg";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            // Wait for output
            while (!File.Exists("../Yolo/test/output/0_0.jpg"))
            {
                System.Threading.Thread.Sleep(500);
            }
            strCmdText = "mv ../Yolo/test/output/0_0.jpg 0_0.jpg";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            using (WebClient client = new WebClient())
            {
                client.UploadFile(output, "0_0.jpg");
            }
        }
    }
    class Program
    {
        // Master IP address
        static string masterIP = "192.168.43.130";

        // Input file
        static string input = @"C:\xampp\htdocs\EdgeLens\Aneka\input.jpg";

        static void Main(string[] args)
        {
            AnekaApplication<AnekaThread, ThreadManager> app = null;
            while (true)
            {
                try
                {
                    // Aneka Configuration
                    Logger.Start();
                    Configuration conf = Configuration.GetConfiguration(@"C:\xampp\htdocs\EdgeLens\Aneka\conf.xml");
                    app = new AnekaApplication<AnekaThread, ThreadManager>(conf);

                    // Wait for input
                    while (!File.Exists(input))
                    {
                        Console.WriteLine("Waiting for input");
                        System.Threading.Thread.Sleep(1000);
                    }

                    HelloWorld hw = new HelloWorld(masterIP);
                    AnekaThread th = new AnekaThread(hw.PrintHello, app);
                    th.Start();
                    th.Join();
                    hw = (HelloWorld)th.Target;
                    Console.WriteLine("Job done");
                    File.Delete(input);
                }
                finally
                {
                    app.StopExecution();
                    Logger.Stop();
                }
            }
        }
    }
}