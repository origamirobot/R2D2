using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Configuration
{
	
	public class R2D2Configuration
	{

		public List<ServoConfiguration> Servos { get; set; }
		public List<PwmConfiguration> PwmControllers { get; set; }
		public DomeMotorConfiguration DomeMotor { get; set; }
		public DriveMotorConfiguration DriveMotorLeft { get; set; }
		public DriveMotorConfiguration DriveMotorRight { get; set; }
	}

}
