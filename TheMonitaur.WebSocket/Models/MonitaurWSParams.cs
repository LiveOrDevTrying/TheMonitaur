using Newtonsoft.Json.Linq;
using WebsocketsSimple.Client.Models;

namespace TheMonitaur.Tcp.Models
{
    public class MonitaurWSParams
    {
        protected string _token;
        protected bool _useSSL;

        public MonitaurWSParams(string token, bool useSSL = true)
        {
            _token = token;
            _useSSL = useSSL;
        }

        public ParamsWSClient ParamsWSClient
        {
            get
            {
                return new ParamsWSClient("connect.themonitaur.com", _useSSL ? 6790 : 6795, _useSSL, _token);
            }
        }
    }
}
