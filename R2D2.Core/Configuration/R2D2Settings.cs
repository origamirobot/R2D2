namespace R2D2.Core.Configuration
{

	/// <summary>
	/// Represents the configuration for the entire R2D2 Unit.
	/// </summary>
	public class R2D2Settings
	{

		/// <summary>
		/// Gets or sets the configuration used for all of the servos.
		/// </summary>
		public List<ServoSettings> Servos { get; set; } = new List<ServoSettings>();

		/// <summary>
		/// Gets or sets the configuration for the PWM controllers.
		/// </summary>
		public List<PwmSettings> PwmControllers { get; set; } = new List<PwmSettings>();

		/// <summary>
		/// Gets or sets the dome motor configuration.
		/// </summary>
		public DomeMotorSettings DomeMotor { get; set; } = new DomeMotorSettings();

		/// <summary>
		/// Gets or sets the left drive motor configuration.
		/// </summary>
		public DriveMotorSettings DriveMotorLeft { get; set; } = new DriveMotorSettings();

		/// <summary>
		/// Gets or sets the right drive motor configuration.
		/// </summary>
		public DriveMotorSettings DriveMotorRight { get; set; } = new DriveMotorSettings();

	}

}
