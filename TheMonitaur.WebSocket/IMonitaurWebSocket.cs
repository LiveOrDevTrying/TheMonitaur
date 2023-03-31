using PHS.Networking.Services;
using System.Threading;
using System.Threading.Tasks;
using TheMonitaur.Lib.Requests;
using TheMonitaur.WebSocket.Events;
using WebsocketsSimple.Core.Models;

namespace TheMonitaur.WebSocket
{
    public interface IMonitaurWebSocket :
        ICoreNetworkingClient<
            MonitaurWSConnectionEventArgs,
            MonitaurWSMessageEventArgs,
            MonitaurWSErrorEventArgs,
            ConnectionWS>
    {
        Task<bool> SendAlertAsync(AlertCreateRequest request, CancellationToken cancellationToken = default);
    }
}