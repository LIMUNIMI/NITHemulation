namespace NITHemulation.Modules.Mouse
{
    /// <summary>
    /// Represents the mouse button flags used for mouse events.
    /// This enumeration uses the <see cref="FlagsAttribute"/> to allow bitwise 
    /// combination of its member values. The flags indicate the state of 
    /// mouse buttons and actions.
    /// </summary>
    [Flags]
    public enum MouseButtonFlags
    {
        /// <summary>
        /// Indicates that the left mouse button is pressed down.
        /// </summary>
        LeftDown = 0x00000002,

        /// <summary>
        /// Indicates that the left mouse button has been released.
        /// </summary>
        LeftUp = 0x00000004,

        /// <summary>
        /// Indicates that the middle mouse button is pressed down.
        /// </summary>
        MiddleDown = 0x00000020,

        /// <summary>
        /// Indicates that the middle mouse button has been released.
        /// </summary>
        MiddleUp = 0x00000040,

        /// <summary>
        /// Indicates that the mouse is moved.
        /// </summary>
        Move = 0x00000001,

        /// <summary>
        /// Indicates that the mouse coordinates are absolute.
        /// </summary>
        Absolute = 0x00008000,

        /// <summary>
        /// Indicates that the right mouse button is pressed down.
        /// </summary>
        RightDown = 0x00000008,

        /// <summary>
        /// Indicates that the right mouse button has been released.
        /// </summary>
        RightUp = 0x00000010
    }
}