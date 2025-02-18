namespace NITHemulation.Modules.Keyboard
{
    /// <summary>
    /// Defines the states of a key press in a keyboard input system.
    /// </summary>
    public static class LKeyPressStates
    {
        /// <summary>
        /// Indicates that a key is being pressed down.
        /// </summary>
        public const string MAKE = "MAKE";

        /// <summary>
        /// Indicates that a key has been released.
        /// </summary>
        public const string BREAK = "BREAK";
    }
}