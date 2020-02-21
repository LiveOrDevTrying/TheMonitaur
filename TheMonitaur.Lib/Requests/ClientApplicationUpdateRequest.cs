using TheMonitaur.Lib.Requests;

namespace TheMonitaur.Lib.Requests
{
    /// <summary>
    /// A Client Application update request
    /// </summary>
    public class ClientApplicationUpdateRequest : UpdateRequest
    {
        /// <summary>
        /// The updated name of the Client Application
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// The updated description of the Client Application
        /// </summary>
        public string ClientDescription { get; set; }
    }
}
