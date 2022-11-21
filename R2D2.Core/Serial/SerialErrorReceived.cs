namespace R2D2.Core.Serial
{

	/// <summary>
	/// Represents the method that will handle the <see cref="E:System.IO.Ports.SerialPort.ErrorReceived"/> event of a <see cref="T:System.IO.Ports.SerialPort"/> object.
	/// </summary>
	/// <param name="sender">The sender of the event, which is the <see cref="T:System.IO.Ports.SerialPort"/> object. </param>
	/// <param name="e">A <see cref="T:System.IO.Ports.SerialErrorReceivedEventArgs"/> object that contains the event data. </param>
	public delegate void SerialErrorReceivedEventHandler(Object sender, SerialErrorReceivedEventArgs e);

	/// <summary>
	/// Reproduced <see cref="System.IO.Ports.SerialErrorReceivedEventArgs"/> for testing purposes.
	/// </summary>
	public class SerialErrorReceivedEventArgs : EventArgs
	{

		/// <summary>
		/// Gets or sets the event type.
		/// </summary>
		public System.IO.Ports.SerialError EventType { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SerialErrorReceivedEventArgs"/> class.
		/// </summary>
		/// <param name="eventCode">The event code.</param>
		public SerialErrorReceivedEventArgs(System.IO.Ports.SerialError eventCode)
		{
			EventType = eventCode;
		}

	}

}
