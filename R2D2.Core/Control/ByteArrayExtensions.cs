namespace R2D2.Core.Control
{


	internal enum MessageInstruction
	{
		Button = 0x01,
		Axis = 0x02,
		Configuration = 0x80,
	}


	/// <summary>
	/// Extension methods for <see cref="Byte"/> arrays.
	/// </summary>
	public static class ByteArrayExtensions
	{


		/// <summary>
		/// Determines whether this byte array has the configuration flag set.
		/// </summary>
		/// <param name="message">The message to check.</param>
		public static Boolean HasConfiguration(this Byte[] message) 
			=> IsFlagSet(message[6], MessageInstruction.Configuration);

		/// <summary>
		/// Determines whether the specified message relates to a button event.
		/// </summary>
		/// <param name="message">The message.</param>
		public static Boolean IsButton(this Byte[] message)
			=> IsFlagSet(message[6], MessageInstruction.Button);

		/// <summary>
		/// Determines whether the specified message relates to an axis event.
		/// </summary>
		/// <param name="message">The message.</param>
		public static Boolean IsAxis(this Byte[] message)
			=> IsFlagSet(message[6], MessageInstruction.Axis);

		/// <summary>
		/// Determines whether the specified message relates to a button press
		/// </summary>
		/// <param name="message">The message.</param>
		public static Boolean IsButtonPressed(this Byte[] message)
			=> message[4] == (Byte)MessageInstruction.Button;

		/// <summary>
		/// Gets the address.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		public static Byte GetAddress(this Byte[] message)
			=> message[7];

		/// <summary>
		/// Gets the axis value.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		public static Int16 GetAxisValue(this Byte[] message)
			=> BitConverter.ToInt16(new Byte[2] { message[4], message[5] }, 0);

		/// <summary>
		/// Determines whether the specified flag set in the provided byte value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="flag">The flag.</param>
		internal static Boolean IsFlagSet(Byte value, MessageInstruction instruction)
		{
			var c = (Byte)(value & (Byte)instruction);
			return c == (Byte)instruction;
		}

	}

}
