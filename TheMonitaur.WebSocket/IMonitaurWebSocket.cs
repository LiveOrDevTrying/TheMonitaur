using System;
using System.Threading.Tasks;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.WebSocket
{
    public interface IMonitaurWebSocket : IDisposable
    {
        Task SendAlertAsync(AlertCreateRequest request);
    }
}