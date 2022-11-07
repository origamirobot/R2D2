namespace R2D2.Core.Configuration
{

	/// <summary>
	/// Represents settings needed to operate a PWM controller.
	/// </summary>
	public class PwmSettings
	{

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets or sets a human readable name.
		/// </summary>
		public String Name { get; private set; } = "Default PWM Controller";

		/// <summary>
		/// Gets or sets the bus identifier this PWM controller is connected to.
		/// </summary>
		public Int32 BusId { get; private set; } = 1;

		/// <summary>
		/// Gets or sets the frequency for which this PWM controller operates.
		/// </summary>
		public Int32 Frequency { get; private set; } = 50;

		/// <summary>
		/// Gets or sets the address of this PWM controller.
		/// </summary> 
		public Int32 Address { get; private set; } = 64;


		#endregion PUBLIC ACCESSORS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="PwmSettings"/> class.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="busId">The bus identifier.</param>
		/// <param name="frequency">The frequency.</param>
		/// <param name="address">The address.</param>
		public PwmSettings(String name, Int32 busId, Int32 frequency, Int32 address)
		{
			Name = name;
			BusId = busId;
			Frequency = frequency;
			Address = address;
		}



		#endregion CONSTRUCTORS

	}

}
