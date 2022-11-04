using Iot.Device.Pwm;
using R2D2.Core.Configuration;

namespace R2D2.Core.PWM
{

	public interface IPwmControllerFactory
	{

		Pca9685 Create(PwmConfiguration settings);

	}

	public class PwmControllerFactory : IPwmControllerFactory
	{
		public Pca9685 Create(PwmConfiguration settings)
		{
			throw new NotImplementedException();
		}
	}

}
