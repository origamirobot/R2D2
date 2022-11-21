using System.IO.Ports;

namespace R2D2.Core.Configuration
{

	/// <summary>
	/// Settings that are used to connect to the sound trigger component.
	/// </summary>
	public class SoundSettings
	{

		public String DeviceFile { get; set; } = "/dev/ttyAMA0";
		public Int32 BaudRate { get; set; }
		public Int32 BitCount { get; set; }
		public StopBits StopBits { get; set; }
		public Parity Parity { get; set; }
		public Int32 WriteTimeout { get; set; }
		public Int32 ReadTimeout { get; set; }

	}
}
