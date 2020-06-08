using System;
using System.Threading.Tasks;
using TheMonitaur.Lib.Enums;
using TheMonitaur.Lib.Requests;
using TheMonitaur.WebSocket;

namespace TheMonitaur.WSClient.Test.SSL
{
    class Program
    {
        private static IMonitaurWebSocket _client;
        private static string _oauthToken;

        static async Task Main(string[] args)
        {
            await MenuAsync();
        }

        static async Task MenuAsync(bool wasIncorrectChoice = false)
        {
            if (wasIncorrectChoice)
            {
                Console.WriteLine($"Please enter a valid selected.");
                Console.WriteLine();
            }

            if (!string.IsNullOrWhiteSpace(_oauthToken))
            {
                Console.WriteLine($"Logged in as: {_oauthToken}");
                Console.WriteLine();
            }

            Console.WriteLine("Select an option below:");
            Console.WriteLine("1 - Set OAuth Token");

            if (!string.IsNullOrWhiteSpace(_oauthToken))
            {
                Console.WriteLine("2 - Send Alert");
            }

            Console.WriteLine("Q. Quit");

            var line = Console.ReadLine();

            switch (line.Trim().ToLower())
            {
                case "1":
                    await SetAccessTokenAsync();
                    break;
                case "2":
                    await SendAlertAsync();
                    break;
                case "q":
                    Environment.Exit(-1);
                    break;
                default:
                    break;
            }
        }

        static async Task SetAccessTokenAsync()
        {
            do
            {
                Console.WriteLine("Enter your access token below:");
                _oauthToken = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(_oauthToken))
                {
                    break;
                }

                Console.WriteLine("That is not a valid entry.");
            } while (string.IsNullOrWhiteSpace(_oauthToken));

            _client = new MonitaurWebSocket(_oauthToken);
            await _client.ConnectAsync();

            Console.WriteLine();

            await MenuAsync();
        }

        static async Task SendAlertAsync()
        {
            var alertType = AlertType.Debug;

            do
            {
                Console.WriteLine("Select your alert type:");

                for (int i = 0; i < Enum.GetValues(typeof(AlertType)).Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {((AlertType)i).ToString()}");
                }

                var alertTypeString = Console.ReadLine();

                if (int.TryParse(alertTypeString, out var alertTypeInt) &&
                    alertTypeInt >= 0 &&
                    alertTypeInt <= Enum.GetValues(typeof(AlertType)).Length)
                {
                    alertType = (AlertType)(alertTypeInt - 1);
                }
                else
                {
                    Console.WriteLine("That is not a valid choice.");
                }
            } while (alertType < 0);


            var statusType = StatusType.Offline;

            do
            {
                Console.WriteLine("Select your status type:");

                for (int i = 0; i < Enum.GetValues(typeof(StatusType)).Length; i++)
                {
                    Console.WriteLine($"{i + 1} - {((StatusType)i).ToString()}");
                }

                var statusTypeString = Console.ReadLine();

                if (int.TryParse(statusTypeString, out var statusTypeInt) &&
                    statusTypeInt >= 0 &&
                    statusTypeInt <= Enum.GetValues(typeof(StatusType)).Length)
                {
                    statusType = (StatusType)(statusTypeInt - 1);
                }
                else
                {
                    Console.WriteLine("That is not a valid choice.");
                }
            } while (statusType < 0);

            Console.WriteLine("Enter the message to include:");

            var message = Console.ReadLine();

            Console.WriteLine("Sending alert ...");

            try
            {
                await _client.SendAlertAsync(new AlertCreateRequest
                {
                    AlertType = alertType,
                    Message = message,
                    StatusType = statusType
                });

                Console.WriteLine("Your alert was sent successfully!");
            }
            catch
            {
                Console.WriteLine("There was a problem sending your alert ... Please try again later ...");
            }

            await MenuAsync();
        }
    }
}
