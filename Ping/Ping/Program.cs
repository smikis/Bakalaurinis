using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ping.Database;
using Ping.Database.Models;

namespace PingComponent
{
    class Program
    {
        private static IDatabaseConnection _database;

        static void Main(string[] args)
        {
            var provider = SetupDependencyInjection();
            _database = provider.GetService<IDatabaseConnection>();

            var timer = new Timer(60 * 1000)
            {
                Enabled = true
            };
            timer.Elapsed += Timer_Elapsed;
            Console.ReadKey();
        }

        private static IConfigurationRoot CreateConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }

        private static ServiceProvider SetupDependencyInjection()
        {
            var configuration = CreateConfiguration();
            //setup our DI
            return new ServiceCollection()
                .AddLogging()
                .AddSingleton<IDatabaseConnection, DatabaseConnection>()
                .AddSingleton(_ => configuration)
                .BuildServiceProvider();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Started writing ping information");
            var internetUsers = _database.GetInternetUsers();
            int timeout = 1024;
            var results = GetPingResults(internetUsers, timeout);
            _database.InsertPingInformation(results.ToList());
            Console.WriteLine("Write ping information");
        }

        private static ConcurrentBag<PingResult> GetPingResults(IEnumerable<InternetUser> internetUsers, int timeout)
        {
            var results = new ConcurrentBag<PingResult>();
            Parallel.ForEach(internetUsers, (internetUser) =>
            {
                System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
                try
                {
                    PingReply reply = pingSender.Send(internetUser.IpAddress, timeout);
                    results.Add(new PingResult
                    {
                        InternetUserId = internetUser.Id,
                        IpAddress = internetUser.IpAddress,
                        Recorded = DateTime.UtcNow,
                        Status = reply.Status.ToString(),
                        Time = reply.RoundtripTime
                    });
                }
                catch (Exception)
                {
                    results.Add(new PingResult
                    {
                        InternetUserId = internetUser.Id,
                        IpAddress = internetUser.IpAddress,
                        Status = "Failed",
                        Recorded = DateTime.UtcNow,
                        Time = 0
                    });
                }
            });
            return results;
        }
    }
  
}


