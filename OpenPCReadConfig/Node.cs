//Team Medjed
//Johnathan Burns, Ethan Spangler, Michael Xie
//Volhacks 2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;

namespace OpenPCReadConfig
{
    class Node
    {
        static System.Timers.Timer t;
        //import every line in config file as a list of strings
        static List<string> lines = new List<string>();
        //current line number increases everytime timer elapses.
        static int curLineNumber = 0;
        static String uuid = "";            //UUID is the computer ID
        static void Main(string[] args)
        {
            using (StreamReader reader = new StreamReader(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDi‌​rectory, "..\\..\\")) + "config"))
            {
                do
                {
                    lines.Add(reader.ReadLine());
                } while (!reader.EndOfStream);
            }
            //Start Timer Setup
            t = new System.Timers.Timer();
            t.AutoReset = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            t.Interval = GetInterval();
            t.Start();
            uuid = lines[curLineNumber];
            curLineNumber++;
            Heartbeat();
            Console.ReadLine(); //Once the user hits Enter, the program will end
        }

        static double GetInterval() //Once every minute
        {
            DateTime now = DateTime.Now;
            return ((60 - now.Second) * 1000 - now.Millisecond);
        }

        //DO NOT CHANGE
        static void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (lines.ElementAtOrDefault(curLineNumber) != null)
            {
                uuid = lines[curLineNumber];
                curLineNumber++;
                Heartbeat();
            }
            else
            {
                Console.WriteLine("End of the file has been reached.");
                Console.WriteLine("The program will close in 5 second.");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
            //Invokes Heartbeat() method once every timer tick (60 sec)
            //t.Interval = GetInterval();
            //t.Start();

        }
        //END OF DO NOT CHANGE

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
