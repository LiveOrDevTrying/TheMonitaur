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
            _client = new WebAPIClient("95c18bed36ec4def92bd3f385cbf6310763242a644b542a08848a9dbadaf6151");

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
