using System.Threading.Tasks;

namespace TheMonitaur.Lib.Events
{
    /// <summary>
    /// The event handler for a Connection Event
    /// </summary>
    /// <param name="sender">The sending object</param>
    /// <param name="args">The specific arguments for the event</param>
    public delegate void ConnectionEventHandler(object sender, ConnectionEventArgs args);
    /// <summary>
    /// The event handler for a Message Event
    /// </summary>
    /// <param name="sender">The sending object</param>
    /// <param name="args">The specific arguments for the event</param>
    public delegate void MessageEventHandler(object sender, MessageEventArgs args);
    /// <summary>
    /// The event handler for an Error Event
    /// </summary>
    /// <param name="sender">The sending object</param>
    /// <param name="args">The specific arguments for the event</param>
    public delegate void ErrorEventHandler(object sender, ErrorEventArgs args);
    /// <summary>
    /// The event handler for receiving an Alert
    /// </summary>
    /// <param name="sender">The sending object</param>
    /// <param name="args">The specific arguments for the event</param>
    public delegate void AlertReceived(object sender, AlertReceivedArgs args);
}
