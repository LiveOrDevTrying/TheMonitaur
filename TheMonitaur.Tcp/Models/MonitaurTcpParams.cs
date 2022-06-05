using Tcp.NET.Client.Models;

namespace TheMonitaur.Tcp.Models
{
    public class MonitaurTcpParams : ParamsTcpClient
    {
        public MonitaurTcpParams(string token,
            string uri = "connect.themonitaur.com",
            int port = 6780,    
            bool isSSL = true) : base(uri, port, "\r\n", isSSL, token)
        {
        }
    }
}
