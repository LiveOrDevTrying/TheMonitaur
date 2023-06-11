using System.Text;
using Tcp.NET.Client.Models;

namespace TheMonitaur.Tcp.Models
{
    public class MonitaurTcpParams : ParamsTcpClient
    {
        protected string _token;
        protected bool _useSSL;

        public MonitaurTcpParams(string token, bool useSSL = true) : base("connect.themonitaur.com", useSSL ? 6780 : 6785, "\r\n", token)
        {
        }
    }
}
