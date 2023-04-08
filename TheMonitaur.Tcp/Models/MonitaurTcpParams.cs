using Tcp.NET.Client.Models;

namespace TheMonitaur.Tcp.Models
{
    public class MonitaurTcpParams
    {
        protected string _token;
        protected bool _useSSL;

        public MonitaurTcpParams(string token, bool useSSL = true)
        {
            _token = token;
            _useSSL = useSSL;
        }

        public ParamsTcpClient ParamsTcpClient
        {
            get
            {
                return new ParamsTcpClient("connect.themonitaur.com", _useSSL ? 6780 : 6785, "\r\n", _useSSL, _token);
            }
        }
    }
}
