using System;
using TheMonitaur.TcpClient;

namespace TheMonitaur.Tests.TcpClient
{
    class Program
    {
        private static IMonitaurTcp _client;

        static void Main(string[] args)
        {
            _client = new MonitaurTcp();

            while (true)
            {
                var line = Console.ReadLine();
                _client.SendMessage(line);
            }
        }
    }
}
