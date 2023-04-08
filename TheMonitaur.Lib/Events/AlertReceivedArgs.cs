using TheMonitaur.Lib.DTOs;

namespace TheMonitaur.Lib.Events
{
    /// <summary>
    /// The event args for when an Alert is received
    /// </summary>
    public class AlertReceivedArgs
    {
        /// <summary>
        /// The alert that was received
        /// </summary>
        public AlertDTO Alert { get; set; }
    }
}
