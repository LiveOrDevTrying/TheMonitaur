using System;
using System.Threading.Tasks;
using TheMonitaur.Lib.Events;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Tcp
{
    public interface IMonitaurTcp : IDisposable
    {
        Task<bool> SendAlertAsync(AlertCreateRequest request);

        Task<bool> ConnectAsync();

        bool Disconnect();

        event ConnectionEventHandler ConnectionEvent;
        event MessageEventHandler MessageEvent;
        event ErrorEventHandler ErrorEvent;

        bool IsRunning { get; }
    }
}