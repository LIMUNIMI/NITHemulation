using System.Drawing;
using NITHlibrary.Tools.Filters.ValueFilters;
using NITHlibrary.Tools.Mappers;
using NITHlibrary.Tools.Timers;

namespace NITHemulation.Modules.Mouse
{
    /// <summary>
    /// A receiver for mouse input which samples the mouse position and velocity at given polling rate.
    /// </summary>
    public class MouseReceiverModule : IDisposable
    {
        /// <summary>
        /// The offset for the FPS X coordinate.
        /// </summary>
        public int FpsOffsetX = 500;

        /// <summary>
        /// The offset for the FPS Y coordinate.
        /// </summary>
        public int FpsOffsetY = 500;

        /// <summary>
        /// The mode in which the mouse receiver operates.
        /// </summary>
        public MousePollingModes MouseMode = MousePollingModes.Normal;

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseReceiverModule"/> class.
        /// </summary>
        /// <param name="pollingMicroseconds">Polling interval in microseconds.</param>
        /// <param name="multiplyFactor">The factor to multiply velocity by.</param>
        /// <param name="MouseMode">The mouse mode.</param>
        /// <param name="MaxVelocityValue">The maximum velocity value.</param>
        public MouseReceiverModule(long pollingMicroseconds = 10000, float multiplyFactor = 1f, MousePollingModes MouseMode = MousePollingModes.Normal, double MaxVelocityValue = 100000f)
        {
            this.VelocityFilterX = VelocityFilterX;
            this.VelocityFilterY = VelocityFilterY;
            this.PollingTimer = new MicroTimer(pollingMicroseconds);
            this.PollingTimer.MicroTimerElapsed += PollingTimer_MicroTimerElapsed;
            this.MultiplyFactor = multiplyFactor;
            this.MaxVelocityValue = MaxVelocityValue;
            this.MouseMode = MouseMode;

            // Initialize filters to bypass
            VelocityFilterX = new DoubleFilterBypass();
            VelocityFilterY = new DoubleFilterBypass();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MouseReceiverModule"/> class.
        /// </summary>
        ~MouseReceiverModule()
        {
            Dispose();
        }

        /// <summary>
        /// List of behaviors to handle mouse samples.
        /// </summary>
        public List<IMouseBehavior> Behaviors { get; set; } = new List<IMouseBehavior>();

        /// <summary>
        /// Gets or sets the X direction component.
        /// </summary>
        public int DirectionX { get; set; }

        /// <summary>
        /// Gets or sets the Y direction component.
        /// </summary>
        public int DirectionY { get; set; }

        /// <summary>
        /// Gets or sets the maximum velocity value.
        /// </summary>
        public double MaxVelocityValue { get; set; }

        /// <summary>
        /// Gets or sets the multiplication factor for velocity.
        /// </summary>
        public float MultiplyFactor { get; set; }

        /// <summary>
        /// Gets or sets the X coordinate for mouse position.
        /// </summary>
        public double PositionX { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate for mouse position.
        /// </summary>
        public double PositionY { get; set; }

        /// <summary>
        /// Gets or sets the filter for X velocity.
        /// </summary>
        public IDoubleFilter VelocityFilterX { get; set; }

        /// <summary>
        /// Gets or sets the filter for Y velocity.
        /// </summary>
        public IDoubleFilter VelocityFilterY { get; set; }

        /// <summary>
        /// Gets or sets the X velocity component.
        /// </summary>
        public double VelocityX { get; set; }

        /// <summary>
        /// Gets or sets the Y velocity component.
        /// </summary>
        public double VelocityY { get; set; }

        /// <summary>
        /// Gets or sets the past X coordinate for mouse position.
        /// </summary>
        private double PastPositionX { get; set; }

        /// <summary>
        /// Gets or sets the past Y coordinate for mouse position.
        /// </summary>
        private double PastPositionY { get; set; }

        /// <summary>
        /// Gets or sets the timer for polling.
        /// </summary>
        private MicroTimer PollingTimer { get; set; }

        /// <summary>
        /// Gets or sets the sample of mouse data.
        /// </summary>
        private MouseDataSample Sample { get; set; }

        /// <summary>
        /// Gets or sets the velocity extractor for X axis.
        /// </summary>
        private VelocityCalculatorBasic VelocityExtractorX { get; set; } = new VelocityCalculatorBasic();

        /// <summary>
        /// Gets or sets the velocity extractor for Y axis.
        /// </summary>
        private VelocityCalculatorBasic VelocityExtractorY { get; set; } = new VelocityCalculatorBasic();

        /// <summary>
        /// Stops the polling timer and releases resources.
        /// </summary>
        public void Dispose()
        {
            PollingTimer.Stop();
        }

        /// <summary>
        /// Gets the current position of the cursor.
        /// </summary>
        /// <returns>The current cursor position as <see cref="Point"/>.</returns>
        public Point GetInstantCursorPosition()
        {
            return MouseFunctions.GetCursorPosition();
        }

        /// <summary>
        /// Sets offset for the FPS mode to the current position of the mouse cursor.
        /// </summary>
        public void SetFpsOffsetToCurrentMousePosition()
        {
            FpsOffsetX = MouseFunctions.GetCursorPosition().X;
            FpsOffsetY = MouseFunctions.GetCursorPosition().Y;
        }

        /// <summary>
        /// Starts polling the mouse position and velocity.
        /// </summary>
        public void StartPolling()
        {
            PollingTimer.Start();
        }

        /// <summary>
        /// Stops polling the mouse position and velocity.
        /// </summary>
        public void StopPolling()
        {
            PollingTimer.Stop();
        }

        /// <summary>
        /// Handles the polling timer elapsed event to track and process mouse position and velocity.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void PollingTimer_MicroTimerElapsed(object sender, MicroTimerEventArgs e)
        {
            switch (MouseMode)
            {
                case MousePollingModes.Normal:   // Mouse is free, but susceptible to screen borders
                    PastPositionX = PositionX;
                    PastPositionY = PositionY;

                    Point pos = MouseFunctions.GetCursorPosition();
                    PositionX = pos.X;
                    PositionY = pos.Y;

                    VelocityExtractorX.Push(PositionX);
                    VelocityExtractorY.Push(PositionY);

                    double TempVelX = VelocityExtractorX.InstantSpeed * MultiplyFactor;
                    double TempVelY = VelocityExtractorY.InstantSpeed * MultiplyFactor;

                    // Check if velocity is more than max
                    if (TempVelX > MaxVelocityValue)
                    {
                        TempVelX = MaxVelocityValue;
                    }
                    if (TempVelY > MaxVelocityValue)
                    {
                        TempVelY = MaxVelocityValue;
                    }

                    VelocityFilterX.Push(TempVelX);
                    VelocityFilterY.Push(TempVelY);

                    VelocityX = VelocityFilterX.Pull();
                    VelocityY = VelocityFilterY.Pull();

                    DirectionX = VelocityExtractorX.Direction;
                    DirectionY = VelocityExtractorY.Direction;

                    Sample = new MouseDataSample(VelocityX, VelocityY, PositionX, PositionY, DirectionX, DirectionY);

                    foreach (IMouseBehavior behavior in Behaviors)
                    {
                        behavior.ReceiveSample(Sample);
                    }
                    return;

                case MousePollingModes.FPS:  // Mouse returns on center every time

                    Point pnt = MouseFunctions.GetCursorPosition();
                    PositionX = pnt.X - FpsOffsetX;
                    PositionY = pnt.Y - FpsOffsetY;

                    DirectionX = Math.Sign(PositionX);
                    DirectionY = Math.Sign(PositionY);

                    double TempVelX2 = Math.Abs(PositionX * MultiplyFactor);
                    double TempVelY2 = Math.Abs(PositionY * MultiplyFactor);

                    // Check if velocity is more than max
                    if (TempVelX2 > MaxVelocityValue)
                    {
                        TempVelX2 = MaxVelocityValue;
                    }
                    if (TempVelY2 > MaxVelocityValue)
                    {
                        TempVelY2 = MaxVelocityValue;
                    }

                    VelocityFilterX.Push(TempVelX2);
                    VelocityFilterY.Push(TempVelY2);

                    VelocityX = VelocityFilterX.Pull();
                    VelocityY = VelocityFilterY.Pull();

                    MouseSender.SetCursorPosition(new Point(FpsOffsetX, FpsOffsetY));     // Return pos to zero

                    Sample = new MouseDataSample(VelocityX, VelocityY, PositionX, PositionY, DirectionX, DirectionY);

                    foreach (IMouseBehavior behavior in Behaviors)
                    {
                        behavior.ReceiveSample(Sample);
                    }

                    return;
            }
        }
    }
}