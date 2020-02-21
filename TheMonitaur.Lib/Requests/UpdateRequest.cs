using System;

namespace TheMonitaur.Lib.Requests
{
    /// <summary>
    /// A base update request
    /// </summary>
    public abstract class UpdateRequest
    {
        /// <summary>
        /// The id of the requested record to update
        /// </summary>
        public long Id { get; set; }
    }
}
