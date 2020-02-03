using System;
using System.Threading.Tasks;
using WebsocketsSimple.Client;
using WebsocketsSimple.Core.Events.Args;

namespace TheMonitaur.WebsocketClient
{
    public class MonitaurWebsocket : IMonitaurWebsocket
    {
        protected readonly IWebsocketClient _client;

        public MonitaurWebsocket(string token, string uri = "localhost", int port = 59460, bool isWSS = false)
        {
            _client = new WebsocketsSimple.Client.WebsocketClient();
            _client.ConnectionEvent += OnConnectionEvent;
            _client.ErrorEvent += OnErrorEvent;
            _client.MessageEvent += OnMessageEvent;

            Task.Run(async () =>
            {
                await _client.ConnectAsync(uri, port, token, isWSS);
            });
        }

        public virtual async Task SendMessageAsync(string message)
        {
            await _client.SendAsync(message);
        }

        protected virtual Task OnMessageEvent(object sender, WSMessageEventArgs args)
        {
            return Task.CompletedTask;
        }
        protected virtual Task OnErrorEvent(object sender, WSErrorEventArgs args)
        {
            return Task.CompletedTask;
        }
        protected virtual Task OnConnectionEvent(object sender, WSConnectionEventArgs args)
        {
            return Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            if (_client != null)
            {
                _client.ConnectionEvent -= OnConnectionEvent;
                _client.ErrorEvent -= OnErrorEvent;
                _client.MessageEvent -= OnMessageEvent;
            }
        }
    }
}
