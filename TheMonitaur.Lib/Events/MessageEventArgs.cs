using System;
using TheMonitaur.Lib.DTOs;
using TheMonitaur.Lib.Enums;

namespace TheMonitaur.Lib.Events
{
    /// <summary>
    /// The abstract base class for a Message event
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// The message event type
        /// </summary>
        public MessageEventType MessageEventType { get; set; }
        /// <summary>
        /// The message content
        /// </summary>
        public string Message { get; set; }
    }
}
