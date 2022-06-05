using WebsocketsSimple.Client.Models;

namespace TheMonitaur.Tcp.Models
{
    public class MonitaurWSParams : ParamsWSClient
    {
        public MonitaurWSParams(string token,
            string uri = "connect.themonitaur.com",
            int port = 6790,    
            bool isSSL = true) : base(uri, port, isSSL, token)
        {
        }
    }
}
