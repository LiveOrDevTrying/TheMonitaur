using Newtonsoft.Json;
using PHS.Networking.Enums;
using PHS.Networking.Models;
using System;
using System.Threading.Tasks;
using TheMonitaur.Lib.Events;
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

        public event ConnectionEventHandler ConnectionEvent;
        public event MessageEventHandler MessageEvent;
        public event ErrorEventHandler ErrorEvent;

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
            _client.ConnectionEvent += OnConnectionEvent;
            _client.MessageEvent += OnMessageEvent;
            _client.ErrorEvent += OnErrorEvent;
        }

        public async Task ConnectAsync()
        {
            try
            {
                await DisconnectAsync();
                await _client.ConnectAsync();
                return;
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(this, new ErrorEventArgs
                {
                    Exception = ex
                });
            }
        }
        public async Task DisconnectAsync()
        {
            if (_client.IsRunning)
            {
                await _client.DisconnectAsync();
            }
        }
        protected virtual Task OnErrorEvent(object sender, WSErrorClientEventArgs args)
        {
            ErrorEvent?.Invoke(this, new ErrorEventArgs
            {
                Exception = args.Exception
            });
            return Task.CompletedTask;
        }
        protected virtual Task OnMessageEvent(object sender, WSMessageClientEventArgs args)
        {
            MessageEvent?.Invoke(this, new MessageEventArgs
            {
                MessageEventType = Lib.Enums.MessageEventType.Inbound,
                Message = args.Message
            });

            return Task.CompletedTask;
        }
        protected virtual Task OnConnectionEvent(object sender, WSConnectionClientEventArgs args)
        {
            switch (args.ConnectionEventType)
            {
                case ConnectionEventType.Connected:
                    ConnectionEvent?.Invoke(this, new ConnectionEventArgs
                    {
                        ConnectionStatusType = Lib.Enums.ConnectionStatusType.Connected
                    });
                    break;
                case ConnectionEventType.Disconnect:
                    ConnectionEvent?.Invoke(this, new ConnectionEventArgs
                    {
                        ConnectionStatusType = Lib.Enums.ConnectionStatusType.Disconnected
                    });
                    break;
                case ConnectionEventType.Connecting:
                    ConnectionEvent?.Invoke(this, new ConnectionEventArgs
                    {
                        ConnectionStatusType = Lib.Enums.ConnectionStatusType.Connecting
                    });
                    break;
                default:
                    break;
            }

            return Task.CompletedTask;
        }

        public virtual async Task SendAlertAsync(AlertCreateRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await _client.SendToServerAsync(new Packet
                {
                    Data = json,
                    Timestamp = DateTime.UtcNow
                });

                MessageEvent?.Invoke(this, new MessageEventArgs
                {
                    Message = json,
                    MessageEventType = Lib.Enums.MessageEventType.Outbound
                });
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(this, new ErrorEventArgs
                {
                    Exception = ex
                });
            }
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
