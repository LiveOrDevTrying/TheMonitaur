using System;
using TheMonitaur.Lib.Enums;

namespace TheMonitaur.Lib.Events
{
    /// <summary>
    /// The abstract base class for a Connection event
    /// </summary>
    public class ConnectionEventArgs : EventArgs
    {
        /// <summary>
        /// The connection status type
        /// </summary>
        public ConnectionStatusType ConnectionStatusType { get; set; }
    }
}
