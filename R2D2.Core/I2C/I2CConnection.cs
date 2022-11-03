namespace R2D2.Core.I2C
{

	/// <summary>
	/// Represents a connection to an I2C device.
	/// </summary>
	public class I2CConnection
	{

		#region PRIVATE PROPERTIES


		private readonly I2CDriver _driver;
		private readonly Int32 _deviceAddress;


		#endregion PRIVATE PROPERTIES

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets the device address.
		/// </summary>
		public Int32 DeviceAddress => _deviceAddress;


		#endregion PUBLIC ACCESSORS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="I2CConnection"/> class.
		/// </summary>
		public I2CConnection(I2CDriver driver, Int32 deviceAddress)
		{
			_driver = driver;
			_deviceAddress = deviceAddress;
		}


		#endregion CONSTRUCTORS

		#region PUBLIC METHODS


		/// <summary>
		/// Executes the specified transaction against the current I2C device.
		/// </summary>
		/// <param name="transaction">The transaction to execute.</param>
		/// <exception cref="System.ArgumentNullException">transaction</exception>
		public void Execute(I2CTransaction transaction)
		{
			if (transaction == null)
				throw new ArgumentNullException(nameof(transaction));
			_driver.Execute(_deviceAddress, transaction);
		}

		/// <summary>
		/// Writes the specified payload to the current I2C device.
		/// </summary>
		/// <param name="payload">The payload to write.</param>
		public void Write(params Byte[] payload)
			=> Execute(new I2CTransaction(new I2CWriteAction(payload)));

		/// <summary>
		/// Writes the specified payload to the current I2C device.
		/// </summary>
		/// <param name="payload">The payload to write.</param>
		public void Write(Byte payload)
			=> Execute(new I2CTransaction(new I2CWriteAction(payload)));

		/// <summary>
		/// Reads the specified number of bytes from the current I2C device.
		/// </summary>
		/// <param name="count">The number of bytes to read.</param>
		/// <returns></returns>
		public Byte[] Read(Int32 count)
		{
			var action = new I2CReadAction(new Byte[count]);
			Execute(new I2CTransaction(action));
			return action.Payload;
		}

		/// <summary>
		/// Reads a single byte from the I2C device.
		/// </summary>
		/// <returns></returns>
		public Byte Read() 
			=> Read(1)[0];


		#endregion PUBLIC METHODS

	}

}
