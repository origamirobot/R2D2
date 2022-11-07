namespace R2D2.Core.Control
{

	/// <summary>
	/// Event arguments that are sent when a button changes it's state.
	/// </summary>
	public class ButtonEventArgs
	{

		#region PUBLIC ACCESSORS


		/// <summary>
		/// Gets the button that changed it's state.
		/// </summary>
		public Byte Button { get; set; }

		/// <summary>
		/// Gets a value indicating whether this button is pressed or not.
		/// </summary>
		public Boolean Pressed { get; set; }


		#endregion PUBLIC ACCESSORS

		#region CONSTRUCTORS


		/// <summary>
		/// Initializes a new instance of the <see cref="ButtonEventArgs" /> class.
		/// </summary>
		/// <param name="button">The button.</param>
		/// <param name="pressed">if set to <c>true</c> [pressed].</param>
		public ButtonEventArgs(Byte button, Boolean pressed)
		{
			Button = button;
			Pressed = pressed;
		}


		#endregion CONSTRUCTORS

	}

}
