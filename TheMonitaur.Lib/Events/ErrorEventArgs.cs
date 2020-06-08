using System;

namespace TheMonitaur.Lib.Events
{
    /// <summary>
    /// The abstract base class for an Error event
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
        /// <summary>
        /// The exception thrown
        /// </summary>
        public Exception Exception { get; set; }
    }
}
