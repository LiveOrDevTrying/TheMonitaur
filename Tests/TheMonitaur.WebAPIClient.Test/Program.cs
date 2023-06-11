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
            _client = new WebAPIClient("acb339de1d6d41378e2d95c067473f07e49eab5aa6f94703be655fbcd50d1a6c");

            _timer = new Timer(TimerCallback, null, 50, 50);

            Console.WriteLine("Running ...");

            for (int j = 0; j < 500; j++)
            {
                Task.Run(async () =>
                {
                    for (int i = 0; i < 500; i++)
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

                Task.Delay(1000);
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
