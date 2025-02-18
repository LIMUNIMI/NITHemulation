using System.Drawing;

namespace NITHemulation.Modules.Mouse
{
    /// <summary>
    /// Provides methods to send various mouse events and emulate mouse functions.
    /// </summary>
    public static class MouseSender
    {
        /// <summary>
        /// Sends a mouse button event.
        /// </summary>
        /// <param name="eventFlag">The flag representing the mouse button event to be sent.</param>
        public static void SendMouseButtonEvent(MouseButtonFlags eventFlag)
        {
            MouseFunctions.MouseEvent(eventFlag);
        }

        /// <summary>
        /// Sends a mouse wheel move event.
        /// </summary>
        /// <param name="speed">The speed of the wheel move.</param>
        public static void SendMouseWheelMove(int speed)
        {
            MouseFunctions.WheelMove(speed);
        }

        /// <summary>
        /// Sets the cursor position to the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates to set the cursor position to.</param>
        public static void SetCursorPosition(Point coordinates)
        {
            MouseFunctions.SetCursorPosition(coordinates.X, coordinates.Y);
        }

        /// <summary>
        /// Sets the visibility of the mouse cursor.
        /// </summary>
        /// <param name="visible">A boolean indicating whether the cursor should be visible.</param>
        public static void SetCursorVisible(bool visible)
        {
            MouseFunctions.ShowMouseCursor(visible);
        }
    }
}