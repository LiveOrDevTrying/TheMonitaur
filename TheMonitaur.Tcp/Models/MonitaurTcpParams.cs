using System.Text;
using Tcp.NET.Client.Models;

namespace TheMonitaur.Tcp.Models
{
    public class MonitaurTcpParams : ParamsTcpClient
    {
        protected string _token;
        protected bool _useSSL;

        public MonitaurTcpParams(string token, bool useSSL = true) : base("connect.themonitaur.pushedtoprod.com", useSSL ? 6425 : 6430, "\r\n", token, useSSL)
        {
        }
    }
}
