using Newtonsoft.Json;
using PHS.Networking.Enums;
using PHS.Networking.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tcp.NET.Client;
using Tcp.NET.Client.Events.Args;
using Tcp.NET.Client.Models;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Tcp
{
    public class MonitaurTcp : IMonitaurTcp
    {
        protected readonly ITcpNETClient _client;
        protected readonly string _token;
        protected readonly int _port;

        public MonitaurTcp(string token,
            string uri = "connect.themonitaur.com",
            int port = 6780,
            bool isSSL = true)
        {
            _token = token;

            var pparameters = new ParamsTcpClient
            {
                EndOfLineCharacters = "\r\n",
                IsSSL = isSSL,
                Port = port,
                Uri = uri
            };

            _client = new TcpNETClient(pparameters, oauthToken: token);
            _client.ConnectionEvent += OnConnectionEvent;
            _client.MessageEvent += OnMessageEvent;
            _client.ErrorEvent += OnErrorEvent;
            _client.ConnectAsync();
        }

        protected virtual async Task OnErrorEvent(object sender, TcpErrorClientEventArgs args)
        {
            if (_client != null &&
                !_client.IsRunning)
            {
                Thread.Sleep(10000);
                await _client.ConnectAsync();
            }

        }
        protected virtual Task OnMessageEvent(object sender, TcpMessageClientEventArgs args)
        {
            return Task.CompletedTask;
        }
        protected virtual async Task OnConnectionEvent(object sender, TcpConnectionClientEventArgs args)
        {
            switch (args.ConnectionEventType)
            {
                case ConnectionEventType.Connected:
                    await _client.SendToServerRawAsync($"oauth:{_token}"); 
                    break;
                case ConnectionEventType.Disconnect:
                    Thread.Sleep(10000);
                    await _client.ConnectAsync();
                    break;
                case ConnectionEventType.Connecting:
                    break;
                default:
                    break;
            }
        }

        public virtual async Task SendAlertAsync(AlertCreateRequest request)
        {
            await _client.SendToServerAsync(new Packet
            {
                Data = JsonConvert.SerializeObject(request),
                Timestamp = DateTime.UtcNow
            });
        }

        public virtual void Dispose()
        {
            _client.Dispose();
            _client.ConnectionEvent -= OnConnectionEvent;
            _client.MessageEvent -= OnMessageEvent;
            _client.ErrorEvent -= OnErrorEvent;
        }
    }
}
