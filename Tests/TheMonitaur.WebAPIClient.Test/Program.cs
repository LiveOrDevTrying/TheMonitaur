using System;
using System.Threading;
using System.Threading.Tasks;
using TheMonitaur.WebAPI;

namespace TheMonitaur.WebAPI.Test
{
    class Program
    {
        private static IWebAPIClient _client;
        private static volatile int _counter;
        private static Timer _timer;
        
        static void Main(string[] args)
        {
            _client = new WebAPIClient("df3d6bfb72e84a43aa3c5d2613e634a179d709c7902e44ca972d64a65b4fd8e0", "https://localhost:44300");

            _timer = new Timer(TimerCallback, null, 50, 50);

            Console.WriteLine("Running ...");

            for (int j = 0; j < 50; j++)
            {
                Task.Run(async () =>
                {
                    for (int i = 0; i < 300; i++)
                    {
                        Console.WriteLine("Counter: " + (++_counter).ToString());
                        await _client.CreateAlertAsync(new Lib.Requests.AlertCreateRequest
                        {
                            AlertType = Lib.Enums.AlertType.Alert,
                            Message = "Test Message",
                            StatusType = Lib.Enums.StatusType.Online
                        });
                    }
                });
            }

            while (true)
            {
                Console.ReadLine();
            }
        }

        static void TimerCallback(object state)
        {
        }
    }
}
