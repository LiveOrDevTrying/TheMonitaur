using System;
using System.Threading.Tasks;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Tcp
{
    public interface IMonitaurTcp : IDisposable
    {
        Task SendAlertAsync(AlertCreateRequest request); 
        
        Task<bool> ConnectAsync();

        Task<bool> DisconnectAsync();
    }
}