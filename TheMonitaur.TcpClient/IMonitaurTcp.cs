using System;
using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Tcp
{
    public interface IMonitaurTcp : IDisposable
    {
        void SendAlert(AlertCreateRequest request);
    }
}