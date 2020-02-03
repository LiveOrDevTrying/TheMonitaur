using System;
using System.Threading.Tasks;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.WebSocket
{
    public interface IMonitaurWebSocketSSL : IDisposable
    {
        Task ConnectAsync();
        Task DisconnectAsync();
        Task SendAlertAsync(AlertCreateRequest request);
    }
}