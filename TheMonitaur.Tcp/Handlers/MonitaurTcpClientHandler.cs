using Tcp.NET.Client.Handlers;
using Tcp.NET.Core.Events.Args;
using Tcp.NET.Core.Models;
using TheMonitaur.Tcp.Events;
using TheMonitaur.Tcp.Models;

namespace TheMonitaur.Tcp.Handlers
{
    public class MonitaurTcpClientHandler :
        TcpClientHandlerBase<
            MonitaurTcpConnectionEventArgs,
            MonitaurTcpMessageEventArgs,
            MonitaurTcpErrorEventArgs,
            MonitaurTcpParams,
            ConnectionTcp>
    {
        public MonitaurTcpClientHandler(MonitaurTcpParams parameters) : base(parameters)
        {
        }

        protected override ConnectionTcp CreateConnection(ConnectionTcp connection)
        {
            return connection;
        }

        protected override MonitaurTcpConnectionEventArgs CreateConnectionEventArgs(TcpConnectionEventArgs<ConnectionTcp> args)
        {
            return new MonitaurTcpConnectionEventArgs
            {
                Connection = args.Connection,
                ConnectionEventType = args.ConnectionEventType
            };
        }

        protected override MonitaurTcpErrorEventArgs CreateErrorEventArgs(TcpErrorEventArgs<ConnectionTcp> args)
        {
            return new MonitaurTcpErrorEventArgs
            {
                Connection = args.Connection,
                Exception = args.Exception,
                Message = args.Message
            };
        }

        protected override MonitaurTcpMessageEventArgs CreateMessageEventArgs(TcpMessageEventArgs<ConnectionTcp> args)
        {
            return new MonitaurTcpMessageEventArgs
            {
                Bytes = args.Bytes,
                Connection = args.Connection,
                Message = args.Message,
                MessageEventType = args.MessageEventType
            };
        }
    }
}
