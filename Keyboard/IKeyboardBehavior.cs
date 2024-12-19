using RawInputProcessor;

namespace NITHdmis.Modules.Keyboard
{
    /// <summary>
    /// Defines a specific behavior for keyboard events.
    /// Implementing classes will provide specific functionality for processing raw input events from the keyboard (e.g. keystrokes).
    /// </summary>
    public interface IKeyboardBehavior
    {
        /// <summary>
        /// Receives and processes a raw input event.
        /// </summary>
        /// <param name="e">The raw input event arguments containing the event data.</param>
        /// <returns>
        /// An integer that could represent the result of processing the event, such as a status code
        /// or an identifier for the handled event.
        /// </returns>
        int ReceiveEvent(RawInputEventArgs e);
    }
}