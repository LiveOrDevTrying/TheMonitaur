using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using TheMonitaur.Lib.Requests;
using TheMonitaur.Tcp.Models;
using TheMonitaur.Websocket.Handlers;
using TheMonitaur.WebSocket.Events;
using WebsocketsSimple.Client;
using WebsocketsSimple.Client.Models;
using WebsocketsSimple.Core.Models;

namespace TheMonitaur.WebSocket
{
    public class MonitaurWebSocket : 
        WebsocketClientBase<
            MonitaurWSConnectionEventArgs,
            MonitaurWSMessageEventArgs,
            MonitaurWSErrorEventArgs,
            ParamsWSClient,
            MonitaurWebsocketClientHandler,
            ConnectionWS>,
        IMonitaurWebSocket
    {
        public MonitaurWebSocket(ParamsWSClient parameters) : base(parameters)
        {
        }

        protected override MonitaurWebsocketClientHandler CreateWebsocketClientHandler()
        {
            return new MonitaurWebsocketClientHandler(_parameters);
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
