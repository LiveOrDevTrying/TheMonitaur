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
        protected readonly string _token;
        protected readonly int _port;

        public event ConnectionEventHandler ConnectionEvent;
        public event MessageEventHandler MessageEvent;
        public event ErrorEventHandler ErrorEvent;

        public MonitaurTcp(string token,
            string uri = "connect.themonitaur.com",
            int port = 6780,
            bool isSSL = true)
        {
            _token = token;

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
                    await ErrorEvent?.Invoke(this, new ErrorEventArgs
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

                return true;
            }
            catch (Exception ex)
            {
                if (ErrorEvent != null)
                {
                    await ErrorEvent?.Invoke(this, new ErrorEventArgs
                    {
                        Exception = ex
                    });
                }
            }

            return false;
        }

        protected virtual async Task OnErrorEvent(object sender, TcpErrorClientEventArgs args)
        {
            if (ErrorEvent != null)
            {
                await ErrorEvent?.Invoke(this, new ErrorEventArgs
                {
                    Exception = args.Exception
                });
            }
        }
        protected virtual async Task OnMessageEvent(object sender, TcpMessageClientEventArgs args)
        {
            switch (args.MessageEventType)
            {
                case MessageEventType.Sent:
                    if (MessageEvent != null)
                    {
                        await MessageEvent?.Invoke(this, new MessageEventArgs
                        {
                            MessageEventType = Lib.Enums.MessageEventType.Outbound,
                            Message = args.Message
                        });
                    }
                    break;
                case MessageEventType.Receive:
                    if (args.Message == "You are successfully connected to The Monitaur over Tcp.")
                    {
                        if (ConnectionEvent != null)
                        {
                            await ConnectionEvent?.Invoke(this, new ConnectionEventArgs
                            {
                                ConnectionStatusType = Lib.Enums.ConnectionStatusType.Connected
                            });
                        }
                    }
                    else
                    {
                        if (MessageEvent != null)
                        {
                            await MessageEvent?.Invoke(this, new MessageEventArgs
                            {
                                MessageEventType = Lib.Enums.MessageEventType.Inbound,
                                Message = args.Message
                            });
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        protected virtual async Task OnConnectionEvent(object sender, TcpConnectionClientEventArgs args)
        {
            switch (args.ConnectionEventType)
            {
                case ConnectionEventType.Connected:
                    break;
                case ConnectionEventType.Disconnect:
                    if (ConnectionEvent != null)
                    {
                        await ConnectionEvent?.Invoke(this, new ConnectionEventArgs
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
                    await ErrorEvent?.Invoke(this, new ErrorEventArgs
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
