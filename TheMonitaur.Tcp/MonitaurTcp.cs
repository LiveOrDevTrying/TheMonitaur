using Newtonsoft.Json;
using PHS.Networking.Enums;
using System.Threading;
using System.Threading.Tasks;
using Tcp.NET.Client;
using Tcp.NET.Client.Models;
using Tcp.NET.Core.Models;
using TheMonitaur.Lib.Requests;
using TheMonitaur.Tcp.Events;
using TheMonitaur.Tcp.Handlers;
using TheMonitaur.Tcp.Models;

namespace TheMonitaur.Tcp
{
    public class MonitaurTcp : 
        TcpNETClientBase<
            MonitaurTcpConnectionEventArgs,
            MonitaurTcpMessageEventArgs,
            MonitaurTcpErrorEventArgs,
            ParamsTcpClient,
            MonitaurTcpClientHandler,
            ConnectionTcp>, 
        IMonitaurTcp
    {
        public MonitaurTcp(MonitaurTcpParams parameters) : base(parameters.ParamsTcpClient)
        {
        }

        protected override MonitaurTcpClientHandler CreateTcpClientHandler()
        {
            return new MonitaurTcpClientHandler(_parameters);
        }

        protected override void OnMessageEvent(object sender, MonitaurTcpMessageEventArgs args)
        {
            switch (args.MessageEventType)
            {
                case MessageEventType.Sent:
                    break;
                case MessageEventType.Receive:
                    if (args.Message == "You are successfully connected to The Monitaur over Tcp.")
                    {
                        FireEvent(this, new MonitaurTcpConnectionEventArgs
                        {
                            Connection = args.Connection,
                            ConnectionEventType = ConnectionEventType.Connected
                        });

                        return;
                    }
                    break;
                default:
                    break;
            }

            base.OnMessageEvent(this, args);
        }
        protected override void OnConnectionEvent(object sender, MonitaurTcpConnectionEventArgs args)
        {
            switch (args.ConnectionEventType)
            {
                case ConnectionEventType.Connected:
                    return;
                case ConnectionEventType.Disconnect:
                    break;
                default:
                    break;
            }

            base.OnConnectionEvent(this, args);
        }

        public virtual async Task<bool> SendAlertAsync(AlertCreateRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Message.Trim().Length > 255)
            {
                throw new System.Exception("Max message length is 255 characters");
            }

            return await SendAsync(JsonConvert.SerializeObject(request), cancellationToken);
        }
    }
}
