using TheMonitaur.Tcp.Models;
using TheMonitaur.WebSocket.Events;
using WebsocketsSimple.Client.Models;
using WebsocketsSimple.Core.Events.Args;
using WebsocketsSimple.Core.Models;

namespace TheMonitaur.Websocket.Handlers
{
    public class MonitaurWebsocketClientHandler :
        WebsocketClientHandlerBase<
            MonitaurWSConnectionEventArgs,
            MonitaurWSMessageEventArgs,
            MonitaurWSErrorEventArgs,
            ParamsWSClient,
            ConnectionWS>
    {
        public MonitaurWebsocketClientHandler(ParamsWSClient parameters) : base(parameters)
        {
        }

        protected override ConnectionWS CreateConnection(ConnectionWS connection)
        {
            return connection;
        }

        protected override MonitaurWSConnectionEventArgs CreateConnectionEventArgs(WSConnectionEventArgs<ConnectionWS> args)
        {
            return new MonitaurWSConnectionEventArgs
            {
                Connection = args.Connection,
                ConnectionEventType = args.ConnectionEventType
            };
        }

        protected override MonitaurWSErrorEventArgs CreateErrorEventArgs(WSErrorEventArgs<ConnectionWS> args)
        {
            return new MonitaurWSErrorEventArgs
            {
                Connection = args.Connection,
                Exception = args.Exception,
                Message = args.Message
            };
        }

        protected override MonitaurWSMessageEventArgs CreateMessageEventArgs(WSMessageEventArgs<ConnectionWS> args)
        {
            return new MonitaurWSMessageEventArgs
            {
                Bytes = args.Bytes,
                Connection = args.Connection,
                Message = args.Message,
                MessageEventType = args.MessageEventType
            };
        }
    }
}
