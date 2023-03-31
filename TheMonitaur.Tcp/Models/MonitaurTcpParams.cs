using Tcp.NET.Client.Models;

namespace TheMonitaur.Tcp.Models
{
    public class MonitaurTcpParams : ParamsTcpClient
    {
        public MonitaurTcpParams(string token,
            bool useSSL = true) : base("connect.themonitaur.com", useSSL ? 6780 : 6785, "\r\n", useSSL, token)
        {
        }
    }
}
