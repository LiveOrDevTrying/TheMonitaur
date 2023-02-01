namespace TheMonitaur.Lib.Requests
{
    /// <summary>
    /// An Alerts selected request
    /// </summary>
    public class AlertsSelectedRequest
    {
        /// <summary>
        /// The Ids of the selected Alerts
        /// </summary>
        public long[] Ids { get; set; }
    }
}
