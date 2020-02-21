using System;
using TheMonitaur.Lib.Enums;

namespace TheMonitaur.Lib.DTOs
{
    /// <summary>
    /// An Alert data-transfer object
    /// </summary>
    public class AlertDTO : BaseDTO
    {
        /// <summary>
        /// The status type of the Alert
        /// Possible values: 0 - 'Online', 1 - 'Offline'
        /// </summary>
        public StatusType StatusType { get; set; }
        /// <summary>
        /// The status type of the Alert as a string
        /// </summary>
        public string StatusTypeValue { get; set; }
        /// <summary>
        /// The alert type of the Alert,
        /// Possible values: 0 - 'Debug', 1 - 'Info', 2 - 'Warning', 3 - 'Alert', 4 - 'Error'
        /// </summary>
        public AlertType AlertType { get; set; }
        /// <summary>
        /// The alert type of the Alert as a string
        /// </summary>
        public string AlertTypeValue { get; set; }
        /// <summary>
        /// The message or payload attached to the Alert
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// The Timestamp of the alert
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
