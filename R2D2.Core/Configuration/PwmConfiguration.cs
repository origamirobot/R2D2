using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Configuration
{

	public class PwmConfiguration
	{

		public String Name { get; set; }
		public Int32 BusId { get; set; }
		public Int32 Frequency { get; set; }
		public Int32 Address { get; set; }

	}

}
