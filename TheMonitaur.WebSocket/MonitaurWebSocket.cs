using Newtonsoft.Json;
using System.Threading.Tasks;
using TheMonitaur.Lib.Requests;
using TheMonitaur.Tcp.Models;
using TheMonitaur.Websocket.Handlers;
using TheMonitaur.WebSocket.Events;
using WebsocketsSimple.Client;
using WebsocketsSimple.Core.Models;

namespace TheMonitaur.WebSocket
{
    public class MonitaurWebSocket : 
        WebsocketClientBase<
            MonitaurWSConnectionEventArgs,
            MonitaurWSMessageEventArgs,
            MonitaurWSErrorEventArgs,
            MonitaurWSParams,
            MonitaurWebsocketClientHandler,
            ConnectionWS>,
        IMonitaurWebSocket
    {
        public MonitaurWebSocket(MonitaurWSParams parameters) : base(parameters)
        {
        }

        protected override MonitaurWebsocketClientHandler CreateWebsocketClientHandler()
        {
            return new MonitaurWebsocketClientHandler(_parameters);
        }

        public virtual async Task<bool> SendAlertAsync(AlertCreateRequest request)
        {
            return await SendAsync(JsonConvert.SerializeObject(request));
        }

    }
}
