using System;
using System.Net.NetworkInformation;

namespace PingComponent
{
    class Program
    {
        static void Main(string[] args)
        {
            Ping pingSender = new System.Net.NetworkInformation.Ping();
            var target = "8.8.8.8";
            int timeout = 1024;
            Console.WriteLine($"Pinging {target}");
            PingReply reply = pingSender.Send(target, timeout);
            Console.WriteLine("Hello World!");
        }
    }
}
