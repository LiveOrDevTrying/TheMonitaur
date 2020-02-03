using TheMonitaur.Lib.Enums;

namespace TheMonitaur.Lib.Requests
{
    /// <summary>
    /// An Alert create request
    /// </summary>
    public class AlertCreateRequest : CreateRequest
    {
        /// <summary>
        /// The status type of the Alert
        /// Possible values: 0 - 'Online', 1 - 'Offline'
        /// </summary>
        public StatusType StatusType { get; set; }
        /// <summary>
        /// The alert type of the Alert
        /// Possible values: 0 - 'Debug', 1 - 'Info', 2 - 'Warning', 3 - 'Alert', 4 - 'Error'
        /// </summary>
        public AlertType AlertType { get; set; }
        /// <summary>
        /// The message or payload attached to the Alert
        /// </summary>
        public string Message { get; set; }
    }
}
