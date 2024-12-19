using RawInputProcessor;
using System.Windows.Forms;

namespace NITHdmis.Modules.Keyboard
{
    /// <summary>
    /// Represents a module that handles keyboard input through raw input processing.
    /// This class is responsible for initializing the raw input capture and managing
    /// keyboard behavior modules that react to keyboard events.
    /// </summary>
    public class KeyboardModuleForms
    {
        private readonly RawFormsInput _rawinput;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardModuleForms"/> class.
        /// Sets up the raw input processor to capture keyboard events for the specified parent window.
        /// </summary>
        /// <param name="parentForm">The parent form that will receive keyboard input.</param>
        /// <param name="rawInputCaptureMode">The raw input capture mode to use.</param>
        public KeyboardModuleForms(Form parentForm, RawInputCaptureMode rawInputCaptureMode)
        {
            _rawinput = new RawFormsInput(parentForm.Handle, rawInputCaptureMode);
            _rawinput.AddMessageFilter();
            _rawinput.KeyPressed += OnKeyPressed;
        }

        /// <summary>
        /// Gets or sets the collection of keyboard behavior modules that define the actions
        /// to perform in response to keyboard events.
        /// </summary>
        public List<IKeyboardBehavior> KeyboardBehaviors { get; set; } = new List<IKeyboardBehavior>();

        /// <summary>
        /// Event handler that is called when a key is pressed.
        /// It notifies all registered keyboard behavior modules of the key press event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An instance of <see cref="RawInputEventArgs"/> containing event data.</param>
        private void OnKeyPressed(object sender, RawInputEventArgs e)
        {
            foreach (IKeyboardBehavior behavior in KeyboardBehaviors)
            {
                behavior.ReceiveEvent(e);
            }
        }
    }
}