namespace R2D2.Core.Configuration
{

	/// <summary>
	/// Configuration values for a single servo motor.
	/// </summary>
	public class ServoSettings
	{

		/// <summary>
		/// Human friendly name for this servo
		/// </summary>
		public String Name { get; set; } = "Default Servo";

		/// <summary>
		/// The identifier of the bus this servo is connected to.
		/// </summary>
		public Int32 BusId { get; set; }

		/// <summary>
		/// The max rotation of this servo.
		/// </summary>
		public Int32 MaxRotation { get; set; }

		/// <summary>
		/// The minimum pulse width for this servo.
		/// </summary>
		public Int32 MinPulseWidth { get; set; }

		/// <summary>
		/// The maximum pulse width for this servo.
		/// </summary>
		public Int32 MaxPulseWidth { get; set; }

		/// <summary>
		/// The channel this servo is connected to.
		/// </summary>
		public Int32 Channel { get; set; }

		/// <summary>
		/// Gets or sets the angle this servo would need to be in to consider the panel/door it's controlling "open".
		/// </summary>
		public Int32 OpenAngle { get; set; }

		/// <summary>
		/// Gets or sets the angle this servo would need to be in to consider the panel/door it's controlling "closed".
		/// </summary>
		public Int32 CloseAngle { get; set; }

		/// <summary>
		/// Gets or sets the address of this servo's PWM controller.
		/// </summary> 
		public Int32? ControllerAddress { get; set; }

	}

}
