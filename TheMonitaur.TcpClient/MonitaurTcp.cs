using Newtonsoft.Json;
using PHS.Core.Enums;
using PHS.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tcp.NET.Client;
using Tcp.NET.Core.Enums;
using Tcp.NET.Core.Events.Args;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Tcp
{
    public class MonitaurTcp : IMonitaurTcp
    {
        protected readonly ITcpNETClient _client;
        protected readonly string _oauthToken;
        protected readonly string _uri;
        protected readonly int _port;
        protected readonly string _eol;

        public MonitaurTcp(string oauthToken, 
            string uri = "connect.themonituar.com", 
            int port = 6895, 
            string endOfLineCharacters = "\r\n")
        {
            _oauthToken = oauthToken;
            _uri = uri;
            _port = port;
            _eol = endOfLineCharacters;

            _client = new TcpNETClient();
            _client.ConnectionEvent += ConnectionEvent;
            _client.MessageEvent += OnMessageEvent;
            _client.ErrorEvent += OnErrorEvent;
            _client.Connect(uri, port, endOfLineCharacters);
        }

        protected virtual Task OnErrorEvent(object sender, TcpErrorEventArgs args)
        {
            if (_client != null &&
                !_client.IsRunning)
            {
                Thread.Sleep(10000);
                _client.Connect(_uri, _port, _eol);
            }

            return Task.CompletedTask;
        }
        protected virtual Task OnMessageEvent(object sender, TcpMessageEventArgs args)
        {
            return Task.CompletedTask;
        }
        protected virtual Task ConnectionEvent(object sender, TcpConnectionEventArgs args)
        {
            switch (args.ConnectionType)
            {
                case TcpConnectionType.Connected:
                    _client.SendToServer($"oauth:{_oauthToken}");
                    break;
                case TcpConnectionType.Disconnect:
                    Thread.Sleep(10000);
                    _client.Connect(_uri, _port, _eol);
                    break;
                case TcpConnectionType.ServerStart:
                    break;
                case TcpConnectionType.ServerStop:
                    break;
                case TcpConnectionType.Connecting:
                    break;
                default:
                    break;
            }

            return Task.CompletedTask;
        }

        public virtual void SendAlert(AlertCreateRequest request)
        {
            _client.SendToServer(new PacketDTO
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
