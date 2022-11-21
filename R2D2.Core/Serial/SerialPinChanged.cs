using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Serial
{

	/// <summary>
	/// Provides data for the <see cref="E:System.IO.Ports.SerialPort.PinChanged"/> event.
	/// </summary>
	public class SerialPinChangedEventArgs : EventArgs
	{

		/// <summary>
		/// Gets or sets the event type.
		/// </summary>
		/// <returns>
		/// One of the <see cref="T:System.IO.Ports.SerialPinChange"/> values.
		/// </returns>
		public System.IO.Ports.SerialPinChange EventType { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SerialPinChangedEventArgs"/> class.
		/// </summary>
		/// <param name="eventCode">The event code.</param>
		public SerialPinChangedEventArgs(System.IO.Ports.SerialPinChange eventCode)
		{
			EventType = eventCode;
		}

	}

	/// <summary>
	/// Represents the method that will handle the <see cref="E:System.IO.Ports.SerialPort.PinChanged"/> event of a <see cref="T:System.IO.Ports.SerialPort"/> object.
	/// </summary>
	/// <param name="sender">The source of the event, which is the <see cref="T:System.IO.Ports.SerialPort"/> object. </param>
	/// <param name="e">A <see cref="T:System.IO.Ports.SerialPinChangedEventArgs"/> object that contains the event data. </param>
	public delegate void SerialPinChangedEventHandler(Object sender, SerialPinChangedEventArgs e);

}
