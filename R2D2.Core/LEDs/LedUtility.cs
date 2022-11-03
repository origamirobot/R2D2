using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.LEDs
{
	public static class LedUtility
	{


		public static void Blink()
		{

			var controller = new GpioController(PinNumberingScheme.Board);
			var pin = 10;
			var lightTime = 300;

			controller.OpenPin(pin, PinMode.Output);

			try
			{
				while (true)
				{
					controller.Write(pin, PinValue.High);
					Thread.Sleep(lightTime);
					controller.Write(pin, PinValue.Low);
					Thread.Sleep(lightTime);
				}
			}
			finally
			{
				controller.ClosePin(pin);

			}


		}

	}
}
