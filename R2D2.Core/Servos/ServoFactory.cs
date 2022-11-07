using Iot.Device.ServoMotor;
using R2D2.Core.Configuration;
using System.Device.Pwm;

namespace R2D2.Core.Servos
{

	/// <summary>
	/// 
	/// </summary>
	public interface IServoFactory
	{

		/// <summary>
		/// Creates a <see cref="ServoMotor"/> with the properties specified.
		/// </summary>
		/// <param name="channel">The channel to create the servo on.</param>
		/// <param name="settings">The settings to create the servo with.</param>
		/// <returns></returns>
		ServoMotor Create(PwmChannel channel, ServoSettings settings);

	}

	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="R2D2.Core.Servos.IServoFactory" />
	public class ServoFactory: IServoFactory
	{


		/// <summary>
		/// Creates a <see cref="ServoMotor"/> with the properties specified.
		/// </summary>
		/// <param name="channel">The channel to create the servo on.</param>
		/// <param name="settings">The settings to create the servo with.</param>
		/// <returns></returns>
		public ServoMotor Create(PwmChannel channel, ServoSettings settings)
		{
			var servo = new ServoMotor(channel, settings.MaxRotation, settings.MinPulseWidth, settings.MaxPulseWidth);
			return servo;
		}


	}


}
