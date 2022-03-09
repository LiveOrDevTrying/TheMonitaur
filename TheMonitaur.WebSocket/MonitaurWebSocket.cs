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

            _client = new WebsocketClient(_parameters, token: token);
            _client.ConnectionEvent += OnConnectionEvent;
            _client.MessageEvent += OnMessageEvent;
            _client.ErrorEvent += OnErrorEvent;
        }

        public virtual async Task<bool> ConnectAsync()
        {
            try
            {
                await DisconnectAsync();
                await _client.ConnectAsync();

                if (_client.IsRunning)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (ErrorEvent != null)
                {
                    ErrorEvent?.Invoke(this, new ErrorEventArgs
                    {
                        Exception = ex
                    });
                }
            }

            return false;
        }
        public virtual async Task<bool> DisconnectAsync()
        {
            try
            {
                if (_client.IsRunning)
                {
                    await _client.DisconnectAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(this, new ErrorEventArgs
                {
                    Exception = ex
                });
            }

            return false;
        }
        protected virtual void OnErrorEvent(object sender, WSErrorClientEventArgs args)
        {
            ErrorEvent?.Invoke(this, new ErrorEventArgs
            {
                Exception = args.Exception
            });
        }
        protected virtual void OnMessageEvent(object sender, WSMessageClientEventArgs args)
        {
            switch (args.MessageEventType)
            {
                case MessageEventType.Sent:
                    MessageEvent?.Invoke(this, new MessageEventArgs
                    {
                        MessageEventType = Lib.Enums.MessageEventType.Outbound,
                        Message = args.Packet.Data
                    });
                    break;
                case MessageEventType.Receive:
                    MessageEvent?.Invoke(this, new MessageEventArgs
                    {
                        MessageEventType = Lib.Enums.MessageEventType.Inbound,
                        Message = args.Packet.Data
                    });
                    break;
                default:
                    break;
            }
        }
        protected virtual void OnConnectionEvent(object sender, WSConnectionClientEventArgs args)
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
                    break;
                default:
                    break;
            }
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
            }
            catch (Exception ex)
            {
                ErrorEvent?.Invoke(this, new ErrorEventArgs
                {
                    Exception = ex
                });
            }
        }

        public bool IsRunning
        {
            get
            {
                return _client.IsRunning;
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
