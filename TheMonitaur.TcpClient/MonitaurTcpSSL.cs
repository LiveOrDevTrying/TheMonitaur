using Newtonsoft.Json;
using PHS.Core.Enums;
using PHS.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tcp.NET.Client.SSL;
using Tcp.NET.Core.SSL.Enums;
using Tcp.NET.Core.SSL.Events.Args;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Tcp
{
    public class MonitaurTcpSSL : IMonitaurTcpSSL
    {
        protected readonly ITcpNETClientSSL _client;
        protected readonly string _oauthToken;
        protected readonly string _uri;
        protected readonly int _port;
        protected readonly string _eol;

        public MonitaurTcpSSL(
            string oauthToken,
            string uri = "connect.themonituar.com", 
            int port = 6890, 
            string endOfLineCharacters = "\r\n")
        {
            _oauthToken = oauthToken;
            _uri = uri;
            _port = port;
            _eol = endOfLineCharacters;

            _client = new TcpNETClientSSL();
            _client.ConnectionEvent += ConnectionEvent;
            _client.MessageEvent += OnMessageEvent;
            _client.ErrorEvent += OnErrorEvent;
            _client.Connect(uri, port, endOfLineCharacters);
        }

        protected virtual Task OnErrorEvent(object sender, TcpSSLErrorEventArgs args)
        {
            if (_client != null &&
               !_client.IsRunning)
            {
                Thread.Sleep(10000);
                _client.Connect(_uri, _port, _eol);
            }

            return Task.CompletedTask;
        }
        protected virtual Task OnMessageEvent(object sender, TcpSSLMessageEventArgs args)
        {
            return Task.CompletedTask;
        }
        protected virtual async Task ConnectionEvent(object sender, TcpSSLConnectionEventArgs args)
        {
            switch (args.ConnectionType)
            {
                case TcpSSLConnectionType.Connected:
                    await _client.SendToServerAsync($"oauth:{_oauthToken}");
                    break;
                case TcpSSLConnectionType.Disconnect:
                    Thread.Sleep(10000);
                    _client.Connect(_uri, _port, _eol);
                    break;
                case TcpSSLConnectionType.ServerStart:
                    break;
                case TcpSSLConnectionType.ServerStop:
                    break;
                case TcpSSLConnectionType.Connecting:
                    break;
                default:
                    break;
            }
        }

        public virtual async Task SendAlertAsync(AlertCreateRequest request)
        {
            await _client.SendToServerAsync(new PacketDTO
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
