using System;
using TheMonitaur.Domain.Lib.Enums;
using TheMonitaur.Lib.Enums;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Tcp.Test
{
    class Program
    {
        private static IMonitaurTcp _client;
        private static string _oauthToken;

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu(bool wasIncorrectChoice = false)
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
                    SetAccessToken();
                    break;
                case "2":
                    SendAlert();
                    break;
                case "q":
                    Environment.Exit(-1);
                    break;
                default:
                    break;
            }
        }

        static void SetAccessToken()
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

            _client = new MonitaurTcp(_oauthToken, uri: "localhost");

            Console.WriteLine();

            Menu();
        }

        static void SendAlert()
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
                _client.SendAlert(new AlertCreateRequest
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

            Menu();
        }
    }
}
