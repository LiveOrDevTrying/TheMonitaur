using WebsocketsSimple.Client.Models;

namespace TheMonitaur.Tcp.Models
{
    public class MonitaurWSParams : ParamsWSClient
    {
        public MonitaurWSParams(string token,
            bool useSSL = true) : base("connect.themonitaur.com", useSSL ? 6790 : 6795, useSSL, token)
        {
        }
    }
}
