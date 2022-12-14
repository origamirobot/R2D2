namespace R2D2.Core.I2C
{

	/// <summary>
	/// Performs a read action against an I2C device.
	/// </summary>
	/// <seealso cref="R2D2.Core.I2C.I2CAction" />
	public class I2CReadAction : I2CAction
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="I2CReadAction"/> class.
		/// </summary>
		/// <param name="payload">The payload.</param>
		public I2CReadAction(params Byte[] payload)
			: base(payload) { }

	}

}
