using NITHlibrary.Nith.Internals;
using System.Drawing;

namespace NITHemulation.Modules.Mouse
{
    /// <summary>
    /// A behavior for gaze tracking sensors. Translates gaze coordinates into mouse cursor positions.
    /// </summary>
    public class NithSensorBehavior_GazeToMouse : INithSensorBehavior
    {

        // The required parameters for gaze coordinates: gaze_x and gaze_y
        private readonly List<NithParameters> requiredParams = [NithParameters.gaze_x, NithParameters.gaze_y];

        /// <summary>
        /// Initializes a new instance of the <see cref="NithSensorBehavior_GazeToMouse"/> class.
        /// </summary>
        /// <param name="mouseSender">The module responsible for sending mouse commands.</param>
        /// <param name="enabled">Indicates whether the gaze-to-mouse behavior is enabled.</param>
        public NithSensorBehavior_GazeToMouse( bool enabled = false)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the gaze-to-mouse behavior is enabled.
        /// </summary>
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Handles the incoming sensor data and sets the mouse cursor position based on gaze coordinates.
        /// </summary>
        /// <param name="nithData">The sensor data containing gaze information.</param>
        public void HandleData(NithSensorData nithData)
        {
            if (nithData.ContainsParameters(requiredParams) && Enabled)
            {
                int gaze_x = (int)nithData.GetParameterValue(NithParameters.gaze_x).Value.ValueAsDouble;
                int gaze_y = (int)nithData.GetParameterValue(NithParameters.gaze_y).Value.ValueAsDouble;
                MouseSender.SetCursorPosition(new Point(gaze_x, gaze_y));
            }
        }
    }
}