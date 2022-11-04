using Iot.Device.ServoMotor;
using R2D2.Core.Configuration;

namespace R2D2.Core.Servos
{

	public interface IServoFactory
	{
		ServoMotor Create(ServoConfiguration settings);
	}

	public class ServoFactory: IServoFactory
	{


		public ServoMotor Create(ServoConfiguration settings)
		{
			throw new NotImplementedException();
		}


	}


}
