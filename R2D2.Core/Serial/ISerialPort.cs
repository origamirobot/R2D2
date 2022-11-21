using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R2D2.Core.Serial
{

	/// <summary>
	/// Defines an interface for interacting with the wrapped serial port. This exists for testing purposes.
	/// </summary>
	public interface ISerialPort : IDisposable
	{

		/// <summary>
		/// Gets the state of the Carrier Detect line for the port.
		/// </summary>
		/// <returns>
		/// true if the carrier is detected; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open"/> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close"/> method has been called.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		Boolean CDHolding { get; }

		/// <summary>
		/// Gets the state of the Clear-to-Send line.
		/// </summary>
		/// <returns>
		/// true if the Clear-to-Send line is detected; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open"/> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close"/> method has been called.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		Boolean CtsHolding { get; }

		/// <summary>
		/// Gets or sets the standard length of data bits per byte.
		/// </summary>
		/// <returns>
		/// The data bits length.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.ArgumentOutOfRangeException">The data bits value is less than 5 or more than 8. </exception>
		Int32 DataBits { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether null bytes are ignored when transmitted between the port and the receive buffer.
		/// </summary>
		/// <returns>
		/// true if null bytes are ignored; otherwise false. The default is false.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open"/> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close"/> method has been called.</exception>
		Boolean DiscardNull { get; set; }

		/// <summary>
		/// Gets the state of the Data Set Ready (DSR) signal.
		/// </summary>
		/// <returns>
		/// true if a Data Set Ready signal has been sent to the port; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open"/> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close"/> method has been called.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		Boolean DsrHolding { get; }

		/// <summary>
		/// Gets or sets a value that enables the Data Terminal Ready (DTR) signal during serial communication.
		/// </summary>
		/// <returns>
		/// true to enable Data Terminal Ready (DTR); otherwise, false. The default is false.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception>
		Boolean DtrEnable { get; set; }

		/// <summary>
		/// Gets or sets the byte encoding for pre- and post-transmission conversion of text.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Text.Encoding"/> object. The default is <see cref="T:System.Text.ASCIIEncoding"/>.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.Encoding"/> property was set to null.</exception><exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.Encoding"/> property was set to an encoding that is not <see cref="T:System.Text.ASCIIEncoding"/>, <see cref="T:System.Text.UTF8Encoding"/>, <see cref="T:System.Text.UTF32Encoding"/>, <see cref="T:System.Text.UnicodeEncoding"/>, one of the Windows single byte encodings, or one of the Windows double byte encodings.</exception>
		Encoding Encoding { get; set; }

		/// <summary>
		/// Gets or sets the handshaking protocol for serial port transmission of data.
		/// </summary>
		/// <returns>
		/// One of the <see cref="T:System.IO.Ports.Handshake"/> values. The default is None.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.ArgumentOutOfRangeException">The value passed is not a valid value in the <see cref="T:System.IO.Ports.Handshake"/> enumeration.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		Handshake Handshake { get; set; }

		/// <summary>
		/// Gets a value indicating the open or closed status of the <see cref="T:System.IO.Ports.SerialPort"/> object.
		/// </summary>
		/// <returns>
		/// true if the serial port is open; otherwise, false. The default is false.
		/// </returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen"/> value passed is null.</exception><exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.IsOpen"/> value passed is an empty string ("").</exception>
		Boolean IsOpen { get; }

		/// <summary>
		/// Gets or sets the value used to interpret the end of a call to the <see cref="M:System.IO.Ports.SerialPort.ReadLine"/> and <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)"/> methods.
		/// </summary>
		/// <returns>
		/// A value that represents the end of a line. The default is a line feed, (<see cref="P:System.Environment.NewLine"/>).
		/// </returns>
		/// <exception cref="T:System.ArgumentException">The property value is empty.</exception><exception cref="T:System.ArgumentNullException">The property value is null.</exception>
		String NewLine { get; set; }

		/// <summary>
		/// Gets or sets the parity-checking protocol.
		/// </summary>
		/// <returns>
		/// One of the enumeration values that represents the parity-checking protocol. The default is <see cref="F:System.IO.Ports.Parity.None"/>.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.Parity"/> value passed is not a valid value in the <see cref="T:System.IO.Ports.Parity"/> enumeration.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		Parity Parity { get; set; }

		/// <summary>
		/// Gets or sets the byte that replaces invalid bytes in a data stream when a parity error occurs.
		/// </summary>
		/// <returns>
		/// A byte that replaces invalid bytes.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception>
		Byte ParityReplace { get; set; }

		/// <summary>
		/// Gets or sets the port for communications, including but not limited to all available COM ports.
		/// </summary>
		/// <returns>
		/// The communications port. The default is COM1.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.IO.Ports.SerialPort.PortName"/> property was set to a value with a length of zero.-or-The <see cref="P:System.IO.Ports.SerialPort.PortName"/> property was set to a value that starts with "\\".-or-The port name was not valid.</exception><exception cref="T:System.ArgumentNullException">The <see cref="P:System.IO.Ports.SerialPort.PortName"/> property was set to null.</exception><exception cref="T:System.InvalidOperationException">The specified port is open. </exception>
		String PortName { get; set; }

		/// <summary>
		/// Gets or sets the size of the <see cref="T:System.IO.Ports.SerialPort"/> input buffer.
		/// </summary>
		/// <returns>
		/// The buffer size, in bytes. The default value is 4096.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize"/> value set is less than or equal to zero.</exception><exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize"/> property was set while the stream was open.</exception><exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.ReadBufferSize"/> property was set to an odd integer value. </exception>
		Int32 ReadBufferSize { get; set; }

		/// <summary>
		/// Gets or sets the number of milliseconds before a time-out occurs when a read operation does not finish.
		/// </summary>
		/// <returns>
		/// The number of milliseconds before a time-out occurs when a read operation does not finish.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.ArgumentOutOfRangeException">The read time-out value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout"/>. </exception>
		Int32 ReadTimeout { get; set; }

		/// <summary>
		/// Gets or sets the number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived"/> event occurs.
		/// </summary>
		/// <returns>
		/// The number of bytes in the internal input buffer before a <see cref="E:System.IO.Ports.SerialPort.DataReceived"/> event is fired. The default is 1.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.ReceivedBytesThreshold"/> value is less than or equal to zero. </exception>
		Int32 ReceivedBytesThreshold { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the Request to Send (RTS) signal is enabled during serial communication.
		/// </summary>
		/// <returns>
		/// true to enable Request to Transmit (RTS); otherwise, false. The default is false.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.IO.Ports.SerialPort.RtsEnable"/> property was set or retrieved while the <see cref="P:System.IO.Ports.SerialPort.Handshake"/> property is set to the <see cref="F:System.IO.Ports.Handshake.RequestToSend"/> value or the <see cref="F:System.IO.Ports.Handshake.RequestToSendXOnXOff"/> value.</exception><exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception>
		Boolean RtsEnable { get; set; }

		/// <summary>
		/// Gets or sets the standard number of stopbits per byte.
		/// </summary>
		/// <returns>
		/// One of the <see cref="T:System.IO.Ports.StopBits"/> values.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.StopBits"/> value is  <see cref="F:System.IO.Ports.StopBits.None"/>.</exception><exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		StopBits StopBits { get; set; }

		/// <summary>
		/// Gets or sets the size of the serial port output buffer.
		/// </summary>
		/// <returns>
		/// The size of the output buffer. The default is 2048.
		/// </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize"/> value is less than or equal to zero.</exception><exception cref="T:System.InvalidOperationException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize"/> property was set while the stream was open.</exception><exception cref="T:System.IO.IOException">The <see cref="P:System.IO.Ports.SerialPort.WriteBufferSize"/> property was set to an odd integer value. </exception>
		Int32 WriteBufferSize { get; set; }

		/// <summary>
		/// Gets or sets the number of milliseconds before a time-out occurs when a write operation does not finish.
		/// </summary>
		/// <returns>
		/// The number of milliseconds before a time-out occurs. The default is <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout"/>.
		/// </returns>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.IO.Ports.SerialPort.WriteTimeout"/> value is less than zero and not equal to <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout"/>. </exception>
		Int32 WriteTimeout { get; set; }

		/// <summary>
		/// Represents the method that handles the error event of a <see cref="T:System.IO.Ports.SerialPort"/> object.
		/// </summary>
		event SerialErrorReceivedEventHandler ErrorReceived;

		/// <summary>
		/// Represents the method that will handle the serial pin changed event of a <see cref="T:System.IO.Ports.SerialPort"/> object.
		/// </summary>
		event SerialPinChangedEventHandler PinChanged;

		/// <summary>
		/// Represents the method that will handle the data received event of a <see cref="T:System.IO.Ports.SerialPort"/> object.
		/// </summary>
		event SerialDataReceivedEventHandler DataReceived;

		/// <summary>
		/// Closes the port connection, sets the <see cref="P:System.IO.Ports.SerialPort.IsOpen"/> property to false, and disposes of the internal <see cref="T:System.IO.Stream"/> object.
		/// </summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.- or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		void Close();

		/// <summary>
		/// Discards data from the serial driver's receive buffer.
		/// </summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or -An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open"/> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close"/> method has been called.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		void DiscardInBuffer();

		/// <summary>
		/// Discards data from the serial driver's transmit buffer.
		/// </summary>
		/// <exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.InvalidOperationException">The stream is closed. This can occur because the <see cref="M:System.IO.Ports.SerialPort.Open"/> method has not been called or the <see cref="M:System.IO.Ports.SerialPort.Close"/> method has been called.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		void DiscardOutBuffer();

		/// <summary>
		/// Opens a new serial port connection.
		/// </summary>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied to the port.- or -The current process, or another process on the system, already has the specified COM port open either by a <see cref="T:System.IO.Ports.SerialPort"/> instance or in unmanaged code.</exception><exception cref="T:System.ArgumentOutOfRangeException">One or more of the properties for this instance are invalid. For example, the <see cref="P:System.IO.Ports.SerialPort.Parity"/>, <see cref="P:System.IO.Ports.SerialPort.DataBits"/>, or <see cref="P:System.IO.Ports.SerialPort.Handshake"/> properties are not valid values; the <see cref="P:System.IO.Ports.SerialPort.BaudRate"/> is less than or equal to zero; the <see cref="P:System.IO.Ports.SerialPort.ReadTimeout"/> or <see cref="P:System.IO.Ports.SerialPort.WriteTimeout"/> property is less than zero and is not <see cref="F:System.IO.Ports.SerialPort.InfiniteTimeout"/>. </exception><exception cref="T:System.ArgumentException">The port name does not begin with "COM". - or -The file type of the port is not supported.</exception><exception cref="T:System.IO.IOException">The port is in an invalid state.  - or - An attempt to set the state of the underlying port failed. For example, the parameters passed from this <see cref="T:System.IO.Ports.SerialPort"/> object were invalid.</exception><exception cref="T:System.InvalidOperationException">The specified port on the current instance of the <see cref="T:System.IO.Ports.SerialPort"/> is already open.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		void Open();

		/// <summary>
		/// Reads a number of bytes from the <see cref="T:System.IO.Ports.SerialPort"/> input buffer and writes those bytes into a byte array at the specified offset.
		/// </summary>
		/// 
		/// <returns>
		/// The number of bytes read.
		/// </returns>
		/// <param name="buffer">The byte array to write the input to. </param><param name="offset">The offset in the buffer array to begin writing. </param><param name="count">The number of bytes to read. </param><exception cref="T:System.ArgumentNullException">The buffer passed is null. </exception><exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset"/> or <paramref name="count"/> parameters are outside a valid region of the <paramref name="buffer"/> being passed. Either <paramref name="offset"/> or <paramref name="count"/> is less than zero. </exception><exception cref="T:System.ArgumentException"><paramref name="offset"/> plus <paramref name="count"/> is greater than the length of the <paramref name="buffer"/>. </exception><exception cref="T:System.TimeoutException">No bytes were available to read.</exception>
		Int32 Read(Byte[] buffer, Int32 offset, Int32 count);

		/// <summary>
		/// Synchronously reads one character from the <see cref="T:System.IO.Ports.SerialPort"/> input buffer.
		/// </summary>
		/// 
		/// <returns>
		/// The character that was read.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.- or -No character was available in the allotted time-out period.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		Int32 ReadChar();

		/// <summary>
		/// Reads a number of characters from the <see cref="T:System.IO.Ports.SerialPort"/> input buffer and writes them into an array of characters at a given offset.
		/// </summary>
		/// 
		/// <returns>
		/// The number of characters read.
		/// </returns>
		/// <param name="buffer">The character array to write the input to. </param><param name="offset">The offset in the buffer array to begin writing. </param><param name="count">The number of characters to read. </param><exception cref="T:System.ArgumentException"><paramref name="offset"/> plus <paramref name="count"/> is greater than the length of the buffer.- or -<paramref name="count"/> is 1 and there is a surrogate character in the buffer.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="buffer"/> passed is null. </exception><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset"/> or <paramref name="count"/> parameters are outside a valid region of the <paramref name="buffer"/> being passed. Either <paramref name="offset"/> or <paramref name="count"/> is less than zero. </exception><exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.TimeoutException">No characters were available to read.</exception>
		Int32 Read(Char[] buffer, Int32 offset, Int32 count);

		/// <summary>
		/// Synchronously reads one byte from the <see cref="T:System.IO.Ports.SerialPort"/> input buffer.
		/// </summary>
		/// 
		/// <returns>
		/// The byte, cast to an <see cref="T:System.Int32"/>, or -1 if the end of the stream has been read.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended.- or -No byte was read.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		Int32 ReadByte();

		/// <summary>
		/// Reads all immediately available bytes, based on the encoding, in both the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort"/> object.
		/// </summary>
		/// 
		/// <returns>
		/// The contents of the stream and the input buffer of the <see cref="T:System.IO.Ports.SerialPort"/> object.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		String ReadExisting();

		/// <summary>
		/// Reads up to the <see cref="P:System.IO.Ports.SerialPort.NewLine"/> value in the input buffer.
		/// </summary>
		/// 
		/// <returns>
		/// The contents of the input buffer up to the first occurrence of a <see cref="P:System.IO.Ports.SerialPort.NewLine"/> value.
		/// </returns>
		/// <exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended.- or -No bytes were read.</exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/></PermissionSet>
		String ReadLine();

		/// <summary>
		/// Reads a string up to the specified <paramref name="value"/> in the input buffer.
		/// </summary>
		/// 
		/// <returns>
		/// The contents of the input buffer up to the specified <paramref name="value"/>.
		/// </returns>
		/// <param name="value">A value that indicates where the read operation stops. </param><exception cref="T:System.ArgumentException">The length of the <paramref name="value"/> parameter is 0.</exception><exception cref="T:System.ArgumentNullException">The <paramref name="value"/> parameter is null.</exception><exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		String ReadTo(String value);

		/// <summary>
		/// Writes the specified string to the serial port.
		/// </summary>
		/// <param name="text">The string for output. </param><exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.ArgumentNullException"><paramref name="str"/> is null.</exception><exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		void Write(String text);

		/// <summary>
		/// Writes a specified number of characters to the serial port using data from a buffer.
		/// </summary>
		/// <param name="buffer">The character array that contains the data to write to the port. </param><param name="offset">The zero-based byte offset in the <paramref name="buffer"/> parameter at which to begin copying bytes to the port. </param><param name="count">The number of characters to write. </param><exception cref="T:System.ArgumentNullException">The <paramref name="buffer"/> passed is null. </exception><exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset"/> or <paramref name="count"/> parameters are outside a valid region of the <paramref name="buffer"/> being passed. Either <paramref name="offset"/> or <paramref name="count"/> is less than zero. </exception><exception cref="T:System.ArgumentException"><paramref name="offset"/> plus <paramref name="count"/> is greater than the length of the <paramref name="buffer"/>. </exception><exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		void Write(Char[] buffer, Int32 offset, Int32 count);

		/// <summary>
		/// Writes a specified number of bytes to the serial port using data from a buffer.
		/// </summary>
		/// <param name="buffer">The byte array that contains the data to write to the port. </param><param name="offset">The zero-based byte offset in the <paramref name="buffer"/> parameter at which to begin copying bytes to the port. </param><param name="count">The number of bytes to write. </param><exception cref="T:System.ArgumentNullException">The <paramref name="buffer"/> passed is null. </exception><exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset"/> or <paramref name="count"/> parameters are outside a valid region of the <paramref name="buffer"/> being passed. Either <paramref name="offset"/> or <paramref name="count"/> is less than zero. </exception><exception cref="T:System.ArgumentException"><paramref name="offset"/> plus <paramref name="count"/> is greater than the length of the <paramref name="buffer"/>. </exception><exception cref="T:System.ServiceProcess.TimeoutException">The operation did not complete before the time-out period ended. </exception>
		void Write(Byte[] buffer, Int32 offset, Int32 count);

		/// <summary>
		/// Writes the specified string and the <see cref="P:System.IO.Ports.SerialPort.NewLine"/> value to the output buffer.
		/// </summary>
		/// <param name="text">The string to write to the output buffer. </param><exception cref="T:System.ArgumentNullException">The <paramref name="str"/> parameter is null.</exception><exception cref="T:System.InvalidOperationException">The specified port is not open. </exception><exception cref="T:System.TimeoutException">The <see cref="M:System.IO.Ports.SerialPort.WriteLine(System.String)"/> method could not write to the stream.  </exception>
		void WriteLine(String text);

	}

}
