using Newtonsoft.Json;
using PHS.Networking.Enums;
using PHS.Networking.Models;
using System;
using System.Threading.Tasks;
using Tcp.NET.Client;
using Tcp.NET.Client.Events.Args;
using Tcp.NET.Client.Models;
using TheMonitaur.Lib.Events;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Tcp
{
    public class MonitaurTcp : IMonitaurTcp
    {
        protected readonly ITcpNETClient _client;

        public event ConnectionEventHandler ConnectionEvent;
        public event MessageEventHandler MessageEvent;
        public event ErrorEventHandler ErrorEvent;

        public MonitaurTcp(string token,
            string uri = "connect.themonitaur.com",
            int port = 6780,
            bool isSSL = true)
        {
            var parameters = new ParamsTcpClient
            {
                EndOfLineCharacters = "\r\n",
                IsSSL = isSSL,
                Port = port,
                Uri = uri
            };

            _client = new TcpNETClient(parameters, oauthToken: token);
            _client.ConnectionEvent += OnConnectionEvent;
            _client.MessageEvent += OnMessageEvent;
            _client.ErrorEvent += OnErrorEvent;
        }

        public virtual async Task<bool> ConnectAsync()
        {
            try
            {
                Disconnect();

                await _client.ConnectAsync();

                if (_client.IsRunning)
                {
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
        public virtual bool Disconnect()
        {
            try
            {
                if (_client.IsRunning)
                {
                    _client.Disconnect();
                    return true;
                }

                return true;
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

        protected virtual void OnErrorEvent(object sender, TcpErrorClientEventArgs args)
        {
            if (ErrorEvent != null)
            {
                ErrorEvent?.Invoke(this, new ErrorEventArgs
                {
                    Exception = args.Exception
                });
            }
        }
        protected virtual void OnMessageEvent(object sender, TcpMessageClientEventArgs args)
        {
            switch (args.MessageEventType)
            {
                case MessageEventType.Sent:
                    if (MessageEvent != null)
                    {
                        MessageEvent?.Invoke(this, new MessageEventArgs
                        {
                            MessageEventType = Lib.Enums.MessageEventType.Outbound,
                            Message = args.Packet.Data
                        });
                    }
                    break;
                case MessageEventType.Receive:
                    if (args.Packet.Data == "You are successfully connected to The Monitaur over Tcp.")
                    {
                        if (ConnectionEvent != null)
                        {
                            ConnectionEvent?.Invoke(this, new ConnectionEventArgs
                            {
                                ConnectionStatusType = Lib.Enums.ConnectionStatusType.Connected
                            });
                        }
                    }
                    else
                    {
                        if (MessageEvent != null)
                        {
                            MessageEvent?.Invoke(this, new MessageEventArgs
                            {
                                MessageEventType = Lib.Enums.MessageEventType.Inbound,
                                Message = args.Packet.Data
                            });
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        protected virtual void OnConnectionEvent(object sender, TcpConnectionClientEventArgs args)
        {
            switch (args.ConnectionEventType)
            {
                case ConnectionEventType.Connected:
                    break;
                case ConnectionEventType.Disconnect:
                    if (ConnectionEvent != null)
                    {
                        ConnectionEvent?.Invoke(this, new ConnectionEventArgs
                        {
                            ConnectionStatusType = Lib.Enums.ConnectionStatusType.Disconnected
                        });
                    }
                    break;
                case ConnectionEventType.Connecting:
                    break;
                default:
                    break;
            }
        }

        public virtual async Task<bool> SendAlertAsync(AlertCreateRequest request)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                return await _client.SendToServerAsync(new Packet
                {
                    Data = json,
                    Timestamp = DateTime.UtcNow
                });
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
