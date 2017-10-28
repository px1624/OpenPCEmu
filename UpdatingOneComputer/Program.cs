//Team Medjed
//Johnathan Burns, Ethan Spangler, Michael Xie
//Volhacks 2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

namespace UpdatingOneComputer
{
    class Program
    {
        static string uuid = "68634690-073b-4787-94c6-095ee5bf6bde";
        static void Main(string[] args)
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.AutoReset = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            t.Interval = 10000;
            t.Start();
            Console.ReadLine();
        }

        static void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Heartbeat();
            //Invokes Heartbeat() method once every timer tick (60 sec)
            //t.Interval = GetInterval();
            //t.Start();

        }

        static void Heartbeat()
        {
            Console.WriteLine("Heartbeat Sent...");
            Console.WriteLine($"UUID: {uuid}");
            string url = @"http://ec2-54-86-220-26.compute-1.amazonaws.com/?query=TIMESTAMP " + uuid;   //Update timestamp for uuid

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.GetResponse();
        }
    }
}
