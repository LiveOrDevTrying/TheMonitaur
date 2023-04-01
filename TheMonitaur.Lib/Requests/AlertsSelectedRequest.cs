namespace TheMonitaur.Lib.Requests
{
    /// <summary>
    /// An Alerts dismiss request
    /// </summary>
    public class AlertsSelectedRequest
    {
        /// <summary>
        /// The Ids of the Alerts to dismiss
        /// </summary>
        public long[] Ids { get; set; }
    }
}
