using System;
using System.Threading.Tasks;
using TheMonitaur.WebsocketClient;

namespace TheMonitaur.Tests.WebsocketClient
{
    class Program
    {
        private static IMonitaurWebsocket _client;

        static async Task Main(string[] args)
        {
            _client = new MonitaurWebsocket("fakeToken");

            while (true)
            {
                var line = Console.ReadLine();
                await _client.SendMessageAsync(line);
            }
        }
    }
}
