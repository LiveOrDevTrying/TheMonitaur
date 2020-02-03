using System;
using System.Threading.Tasks;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Tcp
{
    public interface IMonitaurTcpSSL : IDisposable
    {
        Task SendAlertAsync(AlertCreateRequest request);
    }
}