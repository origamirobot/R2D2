using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Serial
{

	/// <summary>
	/// Represents the method that will handle the <see cref="E:System.IO.Ports.SerialPort.DataReceived"/> event of a <see cref="T:System.IO.Ports.SerialPort"/> object.
	/// </summary>
	/// <param name="sender">The sender of the event, which is the <see cref="T:System.IO.Ports.SerialPort"/> object. </param>
	/// <param name="e">A <see cref="T:SerialDataReceivedEventArgs"/> object that contains the event data. </param>
	public delegate void SerialDataReceivedEventHandler(Object sender, SerialDataReceivedEventArgs e);
	
	/// <summary>
	/// 
	/// </summary>
	public class SerialDataReceivedEventArgs
	{

		/// <summary>
		/// Gets the type of the event.
		/// </summary>
		public System.IO.Ports.SerialData EventType { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SerialDataReceivedEventArgs"/> class.
		/// </summary>
		public SerialDataReceivedEventArgs(System.IO.Ports.SerialData eventType)
		{
			EventType = eventType;
		}

	}
}
