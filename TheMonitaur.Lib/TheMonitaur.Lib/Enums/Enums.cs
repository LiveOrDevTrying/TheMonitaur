namespace TheMonitaur.Lib.Enums
{
    /// <summary>
    /// The Alert Status Type
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// The Alert Status Type if the service is online
        /// </summary>
        Online,
        /// <summary>
        /// The Alert Status Type if the service is offline
        /// </summary>
        Offline
    }

    /// <summary>
    /// The Alert Type
    /// </summary>
    public enum AlertType
    {
        /// <summary>
        /// The Alert Type for debug statements
        /// </summary>
        Debug,
        /// <summary>
        /// The Alert Type for info statements
        /// </summary>
        Info,
        /// <summary>
        /// The Alert Type for warning statements
        /// </summary>
        Warning,
        /// <summary>
        /// The Alert Type for alert statements
        /// </summary>
        Alert,
        /// <summary>
        /// The Alert Type for error statements
        /// </summary>
        Error
    }
}
