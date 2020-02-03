using System;
using System.Threading.Tasks;

namespace TheMonitaur.WebsocketClient
{
    public interface IMonitaurWebsocket : IDisposable
    {
        Task SendMessageAsync(string message);
    }
}