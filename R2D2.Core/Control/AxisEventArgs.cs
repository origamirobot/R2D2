namespace R2D2.Core.Control
{

	/// <summary>
	/// Event arguments that are sent when an axis changes it's position.
	/// </summary>
	public class AxisEventArgs
	{

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets the axis that has changed.
		/// </summary>
		public Byte Axis { get; private set; }

		/// <summary>
		/// Gets the new value of the changed axis.
		/// </summary>
		public Int16 Value { get; private set; }


		#endregion PUBLIC ACCESSORS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="AxisEventArgs"/> class.
		/// </summary>
		/// <param name="axis">The axis.</param>
		/// <param name="value">The value.</param>
		public AxisEventArgs(Byte axis, Int16 value)
		{
			Axis = axis;
			Value = value;
		}


		#endregion CONSTRUCTORS

	}

}
