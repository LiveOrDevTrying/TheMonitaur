﻿using Newtonsoft.Json;
using PHS.Networking.Enums;
using PHS.Networking.Models;
using System;
using System.Threading;
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

        public async Task<bool> ConnectAsync()
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
                ErrorEvent?.Invoke(this, new ErrorEventArgs
                {
                    Exception = ex
                });
            }

            return false;
        }
        public async Task<bool> DisconnectAsync()
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
                ErrorEvent?.Invoke(this, new ErrorEventArgs
                {
                    Exception = ex
                });
            }

            return false;
        }

        protected virtual Task OnErrorEvent(object sender, TcpErrorClientEventArgs args)
        {
            ErrorEvent?.Invoke(this, new ErrorEventArgs
            {
                Exception = args.Exception
            });
            return Task.CompletedTask;
        }
        protected virtual Task OnMessageEvent(object sender, TcpMessageClientEventArgs args)
        {
            switch (args.MessageEventType)
            {
                case MessageEventType.Sent:
                    MessageEvent?.Invoke(this, new MessageEventArgs
                    {
                        MessageEventType = Lib.Enums.MessageEventType.Outbound,
                        Message = args.Message
                    });
                    break;
                case MessageEventType.Receive:
                    if (args.Message == "You are successfully connected to The Monitaur over Tcp.")
                    {
                        ConnectionEvent?.Invoke(this, new ConnectionEventArgs
                        {
                            ConnectionStatusType = Lib.Enums.ConnectionStatusType.Connected
                        });
                    }
                    else
                    {
                        MessageEvent?.Invoke(this, new MessageEventArgs
                        {
                            MessageEventType = Lib.Enums.MessageEventType.Inbound,
                            Message = args.Message
                        }); 
                    }
                    break;
                default:
                    break;
            }
           
            return Task.CompletedTask;
        }
        protected virtual Task OnConnectionEvent(object sender, TcpConnectionClientEventArgs args)
        {
            switch (args.ConnectionEventType)
            {
                case ConnectionEventType.Connected:
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

            return Task.CompletedTask;
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
                ErrorEvent?.Invoke(this, new ErrorEventArgs
                {
                    Exception = ex
                });
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
