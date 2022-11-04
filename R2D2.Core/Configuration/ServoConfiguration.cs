using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Configuration
{

	/// <summary>
	/// Configuration values for a single servo motor.
	/// </summary>
	public class ServoConfiguration
	{

		/// <summary>
		/// Human friendly name for this servo
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// The identifier of the bus this servo is connected to.
		/// </summary>
		public Int32 BusId { get; set; }

		/// <summary>
		/// The max rotation of this servo.
		/// </summary>
		public Int32 MaxRotation { get; set; }

		/// <summary>
		/// The minimum pulse width for this servo.
		/// </summary>
		public Int32 MinPulseWidth { get; set; }

		/// <summary>
		/// The maximum pulse width for this servo.
		/// </summary>
		public Int32 MaxPulseWidth { get; set; }

		/// <summary>
		/// The channel this servo is connected to.
		/// </summary>
		public Int32 Channel { get; set; }

	}

}
