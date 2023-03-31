using PHS.Networking.Services;
using System.Threading;
using System.Threading.Tasks;
using Tcp.NET.Core.Models;
using TheMonitaur.Lib.Requests;
using TheMonitaur.Tcp.Events;

namespace TheMonitaur.Tcp
{
    public interface IMonitaurTcp : 
        ICoreNetworkingClient<
            MonitaurTcpConnectionEventArgs, 
            MonitaurTcpMessageEventArgs, 
            MonitaurTcpErrorEventArgs, 
            ConnectionTcp>
    {
        Task<bool> SendAlertAsync(AlertCreateRequest request, CancellationToken cancellationToken = default);
    }
}