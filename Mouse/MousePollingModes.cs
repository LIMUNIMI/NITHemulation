namespace NITHdmis.Modules.Mouse
{
    /// <summary>
    /// Represents the different polling modes for the mouse.
    /// </summary>
    public enum MousePollingModes
    {
        /// <summary>
        /// While the mouse position is sampled, it will be free to move around the screen.
        /// </summary>
        Normal,

        /// <summary>
        /// While the mouse position is sampled, it will be locked to the center of the screen.
        /// </summary>
        FPS
    }
}