using Newtonsoft.Json;
using PHS.Core.Enums;
using PHS.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using TheMonitaur.Lib.Requests;
using WebsocketsSimple.Client;
using WebsocketsSimple.Core.Events.Args;

namespace TheMonitaur.WebSocket
{
    public class MonitaurWebSocketSSL : IMonitaurWebSocketSSL
    {
        protected readonly IWebsocketClient _client;
        protected readonly string _oauthToken;
        protected readonly string _uri;
        protected readonly int _port;

        public MonitaurWebSocketSSL(string oauthToken,
            string uri = "connect.themonituar.com",
            int port = 6880)
        {
            _oauthToken = oauthToken;
            _uri = uri;
            _port = port;

            _client = new WebsocketClient();
            _client.ConnectionEvent += ConnectionEvent;
            _client.MessageEvent += OnMessageEvent;
            _client.ErrorEvent += OnErrorEvent;
        }
        public virtual async Task ConnectAsync()
        {
            await _client.ConnectAsync(_uri, _port, _oauthToken, true);
        }
        public virtual async Task DisconnectAsync()
        {
            if (_client != null)
            {
                await _client.DisconnectAsync();
            }
        }

        protected virtual async Task OnErrorEvent(object sender, WSErrorEventArgs args)
        {
            if (_client != null &&
                !_client.IsRunning)
            {
                Thread.Sleep(10000);
                await _client.ConnectAsync(_uri, _port, _oauthToken, true);
            }
        }
        protected virtual Task OnMessageEvent(object sender, WSMessageEventArgs args)
        {
            return Task.CompletedTask;
        }
        protected virtual async Task ConnectionEvent(object sender, WSConnectionEventArgs args)
        {
            switch (args.ConnectionEventType)
            {
                case ConnectionEventType.Connected:
                    break;
                case ConnectionEventType.Disconnect:
                    Thread.Sleep(10000);
                    await _client.ConnectAsync(_uri, _port, _oauthToken, true);
                    break;
                case ConnectionEventType.ServerStart:
                    break;
                case ConnectionEventType.ServerStop:
                    break;
                case ConnectionEventType.Connecting:
                    break;
                case ConnectionEventType.MaxConnectionsReached:
                    break;
                default:
                    break;
            }
        }

        public virtual async Task SendAlertAsync(AlertCreateRequest request)
        {
            await _client.SendAsync(new PacketDTO
            {
                Action = (int)ActionType.SendToServer,
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
