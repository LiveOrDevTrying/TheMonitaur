using Newtonsoft.Json;
using PHS.Networking.Enums;
using System.Threading;
using System.Threading.Tasks;
using TheMonitaur.Lib.DTOs;
using TheMonitaur.Lib.Events;
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
        private event AlertReceived _alertReceived;

        public MonitaurWebSocket(MonitaurWSParams parameters) : base(parameters.ParamsWSClient)
        {
        }

        protected override MonitaurWebsocketClientHandler CreateWebsocketClientHandler()
        {
            return new MonitaurWebsocketClientHandler(_parameters);
        }

        protected override void OnMessageEvent(object sender, MonitaurWSMessageEventArgs args)
        {
            switch (args.MessageEventType)
            {
                case MessageEventType.Sent:
                    break;
                case MessageEventType.Receive:
                    try
                    {
                        var alert = JsonConvert.DeserializeObject<AlertDTO>(args.Message);

                        if (alert != null)
                        {
                            _alertReceived?.Invoke(this, new AlertReceivedArgs
                            {
                                Alert = alert
                            });
                        }
                    }
                    catch
                    { }

                    break;
                default:
                    break;
            }

            base.OnMessageEvent(sender, args);
        }

        public virtual async Task<bool> SendAlertAsync(AlertCreateRequest request, CancellationToken cancellationToken = default)
        {
            if (request.Message.Trim().Length > 255)
            {
                throw new System.Exception("Max message length is 255 characters");
            }

            return await SendAsync(JsonConvert.SerializeObject(request), cancellationToken);
        }

        public event AlertReceived AlertReceived
        {
            add
            {
                _alertReceived += value;
            }
            remove
            {
                _alertReceived -= value;
            }
        }
    }
}
