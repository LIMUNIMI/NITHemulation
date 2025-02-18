namespace NITHemulation.Modules.Mouse
{
    /// <summary>
    /// Represents a sample data structure for the <see cref="MouseModule"/> class.
    /// This class encapsulates the properties related to the mouse's movement, including
    /// velocity, position, and direction.
    /// </summary>
    public class MouseDataSample
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseDataSample"/> class.
        /// </summary>
        public MouseDataSample()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseDataSample"/> class with specified velocity, position, and direction.
        /// </summary>
        /// <param name="velocityX">The velocity of the mouse on the X-axis.</param>
        /// <param name="velocityY">The velocity of the mouse on the Y-axis.</param>
        /// <param name="positionX">The current position of the mouse on the X-axis.</param>
        /// <param name="positionY">The current position of the mouse on the Y-axis.</param>
        /// <param name="directionX">The direction of the mouse movement on the X-axis.</param>
        /// <param name="directionY">The direction of the mouse movement on the Y-axis.</param>
        public MouseDataSample(double velocityX, double velocityY, double positionX, double positionY, int directionX, int directionY)
        {
            VelocityX = velocityX;
            VelocityY = velocityY;
            PositionX = positionX;
            PositionY = positionY;
            DirectionX = directionX;
            DirectionY = directionY;
        }

        /// <summary>
        /// Gets or sets the velocity of the mouse on the X-axis.
        /// </summary>
        public double VelocityX { get; set; }

        /// <summary>
        /// Gets or sets the velocity of the mouse on the Y-axis.
        /// </summary>
        public double VelocityY { get; set; }

        /// <summary>
        /// Gets or sets the current position of the mouse on the X-axis.
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// Gets or sets the current position of the mouse on the Y-axis.
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// Gets or sets the direction of mouse movement on the X-axis.
        /// </summary>
        public int DirectionX { get; set; }

        /// <summary>
        /// Gets or sets the direction of mouse movement on the Y-axis.
        /// </summary>
        public int DirectionY { get; set; }

    }
}