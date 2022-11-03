namespace R2D2.Core.I2C
{

	/// <summary>
	/// Represents an action that can be performed against an I2C device.
	/// </summary>
	public abstract class I2CAction
	{

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets the payload.
		/// </summary>
		public Byte[] Payload { get; private set; }


		#endregion PUBLIC ACCESSORS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="I2CAction"/> class.
		/// </summary>
		/// <param name="payload">The payload.</param>
		/// <exception cref="System.ArgumentNullException">payload</exception>
		protected I2CAction(params Byte[] payload)
		{
			if (payload == null)
				throw new ArgumentNullException(nameof(payload));
			Payload = payload;
		}


		#endregion CONSTRUCTORS

	}

}
