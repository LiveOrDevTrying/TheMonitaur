using System;

namespace TheMonitaur.Lib.DTOs
{
    /// <summary>
    /// A base data-transfer object
    /// </summary>
    public abstract class BaseDTO
    {
        /// <summary>
        /// The unique identifier of the data-transfer object
        /// </summary>
        public long Id { get; set; }
    }
}
