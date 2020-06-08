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
    /// The Alert Alert Type
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

    /// <summary>
    /// The Connection Status Type
    /// </summary>
    public enum ConnectionStatusType
    {
        /// <summary>
        /// The type when a connection is connected
        /// </summary>
        Connected,
        /// <summary>
        /// The type when a connection is connecting
        /// </summary>
        Connecting,
        /// <summary>
        /// The type when a connection is disconnected
        /// </summary>
        Disconnected,
        /// <summary>
        /// The type when a connection is authorized
        /// </summary>
        Authorized
    }

    /// <summary>
    /// The Message Event Type
    /// </summary>
    public enum MessageEventType
    {
        /// <summary>
        /// The type when a message is being received from the server
        /// </summary>
        Inbound,
        /// <summary>
        /// The type when a message is being sent to the server
        /// </summary>
        Outbound
    }
}
