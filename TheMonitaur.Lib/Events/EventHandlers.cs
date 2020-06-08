using System.Threading.Tasks;

namespace TheMonitaur.Lib.Events
{
    /// <summary>
    /// The event handler for a Connection Event
    /// </summary>
    /// <param name="sender">The sending object</param>
    /// <param name="args">The specific arguments for the event</param>
    /// <returns>As async Task when the event  is invoked</returns>
    public delegate Task ConnectionEventHandler(object sender, ConnectionEventArgs args);
    /// <summary>
    /// The event handler for a Message Event
    /// </summary>
    /// <param name="sender">The sending object</param>
    /// <param name="args">The specific arguments for the event</param>
    /// <returns>As async Task when the event fires</returns> 
    public delegate Task MessageEventHandler(object sender, MessageEventArgs args);
    /// <summary>
    /// The event handler for an Error Event
    /// </summary>
    /// <param name="sender">The sending object</param>
    /// <param name="args">The specific arguments for the event</param>
    /// <returns>As async Task when the event is invoked</returns>
    public delegate Task ErrorEventHandler(object sender, ErrorEventArgs args);
}
