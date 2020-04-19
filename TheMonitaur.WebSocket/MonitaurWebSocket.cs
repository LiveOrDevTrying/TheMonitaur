using Newtonsoft.Json;
using PHS.Networking.Enums;
using PHS.Networking.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using TheMonitaur.Lib.Requests;
using WebsocketsSimple.Client;
using WebsocketsSimple.Client.Events.Args;
using WebsocketsSimple.Client.Models;

namespace TheMonitaur.WebSocket
{
    public class MonitaurWebSocket : IMonitaurWebSocket
    {
        protected readonly IWebsocketClient _client;
        protected readonly IParamsWSClient _parameters;

        public MonitaurWebSocket(string token,
            string uri = "connect.themonitaur.com",
            int port = 6790,
            bool isSSL = true)
        {
            _parameters = new ParamsWSClient
            {
                IsWebsocketSecured = isSSL,
                Port = port,
                Uri = uri
            };

            _client = new WebsocketClient(_parameters, oauthToken: token);
            _client.ConnectionEvent += ConnectionEvent;
            _client.MessageEvent += OnMessageEvent;
            _client.ErrorEvent += OnErrorEvent;

            Task.Run(async () =>
            {
                await _client.ConnectAsync();
            });
        }

        protected virtual async Task OnErrorEvent(object sender, WSErrorClientEventArgs args)
        {
            if (_client != null)
            {
                Thread.Sleep(10000);
                await _client.ConnectAsync();
            }
        }
        protected virtual Task OnMessageEvent(object sender, WSMessageClientEventArgs args)
        {
            return Task.CompletedTask;
        }
        protected virtual async Task ConnectionEvent(object sender, WSConnectionClientEventArgs args)
        {
            switch (args.ConnectionEventType)
            {
                case ConnectionEventType.Connected:
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
            _client.ConnectionEvent -= ConnectionEvent;
            _client.MessageEvent -= OnMessageEvent;
            _client.ErrorEvent -= OnErrorEvent;
        }
    }
}
