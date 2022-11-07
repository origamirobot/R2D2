using Iot.Device.Pwm;
using Iot.Device.ServoMotor;
using R2D2.Core.Configuration;
using System.Device.I2c;

namespace R2D2.Core.PWM
{

	/// <summary>
	/// Defines a contract for creating PWM Controllers.
	/// </summary>
	public interface IPwmControllerFactory
	{

		/// <summary>
		/// Creates a new <see cref="Pca9685"/> PWM Controller using the settings provided.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <returns></returns>
		Pca9685 CreateController(PwmSettings settings);

		/// <summary>
		/// Creates a new <see cref="Pca9685"/> PWM Controller using the settings provided.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="busId">The bus identifier.</param>
		/// <param name="frequency">The frequency.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		Pca9685 CreateController(String name, Int32 busId, Int32 frequency, Int32 address);

	
	}

	/// <summary>
	/// Responsible for creating PWM Controllers.
	/// </summary>
	/// <seealso cref="R2D2.Core.PWM.IPwmControllerFactory" />
	public class PwmControllerFactory : IPwmControllerFactory
	{

		/// <summary>
		/// Creates a new <see cref="Pca9685"/> PWM Controller using the settings provided.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <returns></returns>
		public Pca9685 CreateController(PwmSettings settings)
		{
			var config = new I2cConnectionSettings(settings.BusId, settings.Address);
			var device = I2cDevice.Create(config);
			return new Pca9685(device, settings.Frequency);

		}

		/// <summary>
		/// Creates a new <see cref="Pca9685"/> PWM Controller using the settings provided.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="busId">The bus identifier.</param>
		/// <param name="frequency">The frequency.</param>
		/// <param name="address">The address.</param>
		/// <returns></returns>
		public Pca9685 CreateController(String name, Int32 busId, Int32 frequency, Int32 address)
			=> CreateController(new PwmSettings(name, busId, frequency, address));

	
	}

}
