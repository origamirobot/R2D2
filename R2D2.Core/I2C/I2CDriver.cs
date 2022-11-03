using R2D2.Core.GPIO;

namespace R2D2.Core.I2C
{

	/// <summary>
	/// A driver that is used to communicate with an I2C device.
	/// </summary>
	public class I2CDriver : IDisposable
	{

		#region PRIVATE PROPERTIES


		private readonly Object _lock = new Object();


		#endregion PRIVATE PROPERTIES

		#region PROTECTED PROPERTIES


		/// <summary>
		/// Gets the gpio controller.
		/// </summary>
		protected IGpioController GpioController { get; private set; }

		/// <summary>
		/// Gets the sda pin.
		/// </summary>
		protected Int32 SdaPin { get; private set; }

		/// <summary>
		/// Gets the SCL pin.
		/// </summary>
		protected Int32 SclPin { get; private set; }

		/// <summary>
		/// Gets the device address.
		/// </summary>
		protected Int32 DeviceAddress { get; private set; }


		#endregion PROTECTED PROPERTIES

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="I2CDriver" /> class.
		/// </summary>
		/// <param name="sdaPinNumber">The pin number for SDA communication.</param>
		/// <param name="sclPinNumber">The pin number for SCL communication.</param>
		/// <param name="gpioController">The gpio controller.</param>
		public I2CDriver(Int32 sdaPinNumber, Int32 sclPinNumber, IGpioController gpioController)
		{
			SdaPin = sdaPinNumber;
			SclPin = sclPinNumber;
			GpioController = gpioController;
		}


		#endregion CONSTRUCTORS

		#region PUBLIC METHODS


		/// <summary>
		/// Executes the specified device address.
		/// </summary>
		/// <param name="deviceAddress">The device address.</param>
		/// <param name="transaction">The transaction.</param>
		public void Execute(Int32 deviceAddress, I2CTransaction transaction)
		{
			
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}


		#endregion PUBLIC METHODS

	}

}
