using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Extensions
{

	public static class Int32Extensions
	{

		public static Int32 Normalize(this Int16 value, Int32 inputHigh, Int32 inputLow, Int32 outputHigh, Int32 outputLow)
		{
			if (value > inputHigh || value < inputLow)
				throw new ArgumentOutOfRangeException("The value must be between inputHigh and inputLow");

			return (((value - inputLow) / (inputHigh - inputLow)) * (outputHigh - outputLow)) + outputLow;

		}

		public static Int32 Normalize(this Int32 value, Int32 inputHigh, Int32 inputLow, Int32 outputHigh, Int32 outputLow)
		{
			if (value > inputHigh || value < inputLow)
				throw new ArgumentOutOfRangeException("The value must be between inputHigh and inputLow");

			return (((value - inputLow) / (inputHigh - inputLow)) * (outputHigh - outputLow)) + outputLow;

		}

	}

}
