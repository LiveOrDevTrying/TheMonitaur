using System;
using TheMonitaur.Lib.Enums;

namespace TheMonitaur.Lib.Requests
{
    /// <summary>
    /// An Alerts Lookup request
    /// </summary>
    public class AlertsLookupRequest
    {
        /// <summary>
        /// The maximum number of records to retrieve - if not specified, defaults to 150 records. 
        /// The total number of records to retrieve is limited to 5000 maximum records in one webapi call.
        /// </summary>
        public int? MaxRecordsToRetrieve { get; set; }
        /// <summary>
        /// The Alert Type to retrieve, if provided
        /// Possible values: 0 - 'Debug', 1 - 'Info', 2 - 'Warning', 3 - 'Alert', 4 - 'Error'
        /// </summary>
        public AlertType[] AlertTypes { get; set; } = new AlertType[0];
        /// <summary>
        /// The Status Type to retrieve, if provided
        /// Possible values: 0 - 'Online', 1 - 'Offline'
        /// </summary>
        public StatusType[] StatusTypes { get; set; } = new StatusType[0];
        /// <summary>
        /// The Start Date to retrieve the Alerts , if provided- Inclusive
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// The End Date to retrieve the Alerts, if provided - Exclusive
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// Flag to indicate if the query should include active Alerts
        /// </summary>
        public bool? IncludeActiveAlerts { get; set; }
        /// <summary>
        /// Flag to indicate if the query should include dismissed Alerts
        /// </summary>
        public bool? IncludeDismissedAlerts { get; set; }
    }
}
