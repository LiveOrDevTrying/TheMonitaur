using System;
using System.Threading.Tasks;
using TheMonitaur.Lib.Events;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.WebSocket
{
    public interface IMonitaurWebSocket : IDisposable
    {
        Task SendAlertAsync(AlertCreateRequest request);

        Task<bool> ConnectAsync();

        Task<bool> DisconnectAsync();

        public event ConnectionEventHandler ConnectionEvent;
        public event MessageEventHandler MessageEvent;
        public event ErrorEventHandler ErrorEvent;
    }
}